using System;
using Game.Scripts.Observers;
using Game.Scripts.Player.PlayerStateMachine;
using UnityEngine;

namespace Game.Scripts.World.WorldEvents.DeadZone
{
    public class DeadZoneObserver : MonoBehaviour, IObserver
    {
        #region Statements

        [SerializeField] private Observer _observer;
        
        private PlayerStateMachine _playerStateMachine;

        private void Awake()
        {
            _playerStateMachine = transform.parent.GetComponent<PlayerStateMachine>();
        }

        #endregion

        #region Events

        private void OnEnable()
        {
            _observer.Register(this);
        }

        private void OnDisable()
        {
            _observer.Unregister(this);
        }

        public void OnNotify()
        {
            _playerStateMachine.Dead();
        }

        #endregion
    }
}
