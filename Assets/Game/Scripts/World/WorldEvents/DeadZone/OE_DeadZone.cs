using Game.Scripts._Data.Observer;
using Game.Scripts.Player.PlayerStateMachine;
using UnityEngine;

namespace Game.Scripts.World.WorldEvents.DeadZone
{
    public class OE_DeadZone : Observer
    {
        #region Statements

        [SerializeField] private ObserverEvent _observer;
        
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

        public override void OnNotify()
        {
            _playerStateMachine.Teleport();
        }

        #endregion
    }
}
