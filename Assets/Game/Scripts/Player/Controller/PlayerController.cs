using System;
using System.Collections.Generic;
using Game.Scripts.Generic.Managers;
using Game.Scripts.Interactable;
using UnityEngine;

namespace Game.Scripts.Player.Controller
{
    public class PlayerController : MonoBehaviour
    {
        #region Statements
        
        public static IInteract CurrentInteract { get; private set; }

        private static readonly List<IInteract> Interacts = new();

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
            Interacts.Remove(interact);
            
            SetCurrentInteract();
        }

        private void OnTriggerStay(Collider other)
        {
            SetCurrentInteract();
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
    }
}