using System;
using System.Collections.Generic;
using Game.Scripts.Interactable;
using Game.Scripts.StaticUtilities;
using UnityEngine;

namespace Game.Scripts.Player.Controller
{
    [Serializable]
    public struct TurretsToBuild
    {
        public int TurretIndex;
        public GameObject TurretPrefab;
    }
    
    public class PlayerController : MonoBehaviour
    {
        #region Statements
        
        public static IInteract CurrentInteract { get; private set; }

        private static readonly List<IInteract> Interacts = new();
        
        [SerializeField] private List<TurretsToBuild> _turretsToBuild = new();
        private GameObject _turretsParent;

        private void Awake()
        {
            _turretsParent = GameObject.FindGameObjectWithTag(TagRef.TurretsParent);
        }

        #endregion

        #region Triggers

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent<IInteract>(out var interact)) return;
            Interacts.Add(interact);
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (!other.TryGetComponent<IInteract>(out var interact)) return;
            
            interact.ShowOutline(false);
            Interacts.Remove(interact);
            
            SetCurrentInteract();
        }

        private void OnTriggerStay(Collider other)
        {
            if (!other.TryGetComponent<IInteract>(out var interact)) return;
            if (CurrentInteract == interact) return;
            
            SetCurrentInteract();
            SetOutline(interact);
        }

        #endregion

        #region Functions
        
        public static void RemoveCurrentInteract()
        {
            Interacts.Remove(CurrentInteract);
            CurrentInteract = null;
        }

        private void SetCurrentInteract()
        {
            if (Interacts.Count <= 0)
            {
                CurrentInteract = null;
                return;
            }
            
            CurrentInteract = GetClosestInteract();
        }

        private static void SetOutline(IInteract interact)
        {
            CurrentInteract.ShowOutline(true);
            
            if (interact != CurrentInteract)
                interact.ShowOutline(false);
        }

        private IInteract GetClosestInteract()
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

        public void Temp(int index)
        {
            var trs = CurrentInteract.GetTransform();
            var turret = Instantiate(_turretsToBuild[index].TurretPrefab, trs.position, Quaternion.identity, _turretsParent.transform);

            Interacts.Remove(CurrentInteract);
            CurrentInteract?.Destroy();
            CurrentInteract = null;
        }

        #endregion
    }
}
