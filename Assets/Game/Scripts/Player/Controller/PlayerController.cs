using System;
using System.Collections.Generic;
using Game.Scripts._Data.Observer;
using Game.Scripts._Data.TurretData;
using Game.Scripts.Generic.Managers;
using Game.Scripts.Interactables;
using Game.Scripts.StaticUtilities;
using Game.Scripts.Turrets.TurretUpgrader;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Scripts.Player.Controller
{
    [Serializable]
    public struct TurretsToBuild
    {
        public GameObject TurretPrefab;
        public TurretData TurretData;
    }
    
    public class PlayerController : MonoBehaviour
    {
        #region Statements
        
        public static Action<bool> OnTriggerInteract;
        public static Action OnSendTurretUpgrader;
        
        [Space, Title("Observer")]
        [SerializeField] private ObserverEvent _observerCoins;
        
        [Space, Title("Turrets")]
        [SerializeField] private List<TurretsToBuild> _turretsToBuild = new();
        public List<TurretsToBuild> TurretsToBuild => _turretsToBuild;
        
        private static readonly List<Interactable> Interacts = new();
        private static Interactable CurrentInteract { get; set; }
        
        private GameObject _turretsParent;

        private void Start()
        {
            _turretsParent = GameObject.FindGameObjectWithTag(TagRef.TurretsParent);
            
            Interacts.Clear();
            CurrentInteract = null;
        }

        #endregion

        #region Triggers

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent<Interactable>(out var interactable)) return;
            
            interactable.Enter();
            Interacts.Add(interactable);
            
            SetCurrentInteract();
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (!other.TryGetComponent<Interactable>(out var interactable)) return;
            
            interactable.Exit();
            Interacts.Remove(interactable);
            
            SetCurrentInteract();
        }

        private void OnTriggerStay(Collider other)
        {
            if (Interacts.Count <= 1) return;
            
            SetCurrentInteract();
            SetCurrentInteractOutline();
            SendCurretTurretUpgrader(other);
        }

        #endregion

        #region Functions
        
        public static void Interact()
        {
            if (CurrentInteract == null) return;
            CurrentInteract.Interact();
        }

        private void SetCurrentInteract()
        {
            if (Interacts.Count <= 0)
            {
                OnTriggerInteract?.Invoke(false);
                CurrentInteract = null;
                return;
            }
            
            OnTriggerInteract?.Invoke(true);
            CurrentInteract = GetClosestInteract();
        }

        private static void SetCurrentInteractOutline()
        {
            foreach (var interact in Interacts)
            {
                interact.Exit();
            }
            
            CurrentInteract.Enter();
        }
        
        private static void SendCurretTurretUpgrader(Collider other)
        {
            if (!other.TryGetComponent<TurretUpgrader>(out var turretUpgrader)) return;
            
            OnSendTurretUpgrader?.Invoke();
        }

        private Interactable GetClosestInteract()
        {
            var closestInteract = Interacts[0];
            var closestDistance = Vector3.Distance(transform.position, closestInteract.GetTransform().position);
            
            foreach (var interact in Interacts)
            {
                var distance = Vector3.Distance(transform.position, interact.GetTransform().position);
            
                if (!(distance < closestDistance)) continue;
                
                closestDistance = distance;
                closestInteract = interact;
            }

            return closestInteract;
        }

        #endregion

        #region Turrets

        public void BuildTurret(int index)
        {
            var turretCost = _turretsToBuild[index].TurretData.Cost;
            if (GameManager.Instance.CurrentLevelMooCoins < turretCost) return;
            
            var interactTransform = CurrentInteract.GetTransform();
            var interactPosition = interactTransform.position;
            
            Instantiate(_turretsToBuild[index].TurretPrefab, interactPosition, Quaternion.identity, _turretsParent.transform);
            DestroyTurretsToBuildInRadius(interactPosition, 1.6f);
            
            _observerCoins.Notify(-turretCost);
            
            Interacts.Remove(CurrentInteract);
            
            CurrentInteract.Destroy();
            CurrentInteract = null;
            
            OnTriggerInteract?.Invoke(false);
        }
        
        private static void DestroyTurretsToBuildInRadius(Vector3 turretPosition, float radius)
        {
            var hitColliders = Physics.OverlapSphere(turretPosition, radius);

            foreach (var hitCollider in hitColliders)
            {
                if (!hitCollider.TryGetComponent<Interactable>(out var interactable)) return;
                
                if (interactable is not null && interactable != CurrentInteract)
                {
                    Interacts.Remove(interactable);
                    Destroy(hitCollider.gameObject, 0);
                }
            }
        }

        #endregion
    }
}
