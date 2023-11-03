using System;
using Game.Scripts.Interactables;
using UnityEngine;

namespace Game.Scripts.Turrets.TurretUpgrader
{
    public class TurretUpgrader : Interactable
    {
        #region Statements

        public TurretStateMachine.TurretStateMachine TurretStateMachine { get; private set; }

        private void Awake()
        {
            TurretStateMachine = GetComponent<TurretStateMachine.TurretStateMachine>();
        }

        #endregion
        
        #region Functions

        public override void Interact()
        {
            Debug.Log("TurretUpgrader Interact");
        }

        #endregion
    }
}
