using System;
using System.Collections.Generic;
using Game.Scripts._Data.Observer;
using Game.Scripts._Data.TurretData;
using Game.Scripts.Generic.Managers;
using Game.Scripts.Interactables;
using Game.Scripts.StaticUtilities;
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
        
        [Space, Title("Observer")]
        [SerializeField] private ObserverEvent _observerCoins;
        
        [Space, Title("Turrets")]
        [SerializeField] private List<TurretsToBuild> _turretsToBuild = new();
        public List<TurretsToBuild> TurretsToBuild => _turretsToBuild;
        
        private static readonly List<Interactable> Interacts = new();
        private static Interactable CurrentInteract { get; set; } // TODO: detruire les IInteract autour
        
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
        }

        #endregion

        #region Functions
        
        public static void Interact()
        {
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
            
            var trs = CurrentInteract.GetTransform();
            Instantiate(_turretsToBuild[index].TurretPrefab, trs.position, Quaternion.identity, _turretsParent.transform);
            
            _observerCoins.Notify(-turretCost);
            
            Interacts.Remove(CurrentInteract);
            
            CurrentInteract.Destroy();
            CurrentInteract = null;
            
            OnTriggerInteract?.Invoke(false);
        }

        #endregion
    }
}
