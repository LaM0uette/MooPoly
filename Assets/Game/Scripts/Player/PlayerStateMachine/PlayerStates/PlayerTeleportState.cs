using UnityEngine;

namespace Game.Scripts.Player.PlayerStateMachine.PlayerStates
{
    public class PlayerTeleportState : PlayerBaseState
    {
        #region Statements
        
        private readonly Vector3 _position;

        public PlayerTeleportState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
        {
            _position = Vector3.zero;
        }
        
        public PlayerTeleportState(PlayerStateMachine playerStateMachine, Vector3 postion) : base(playerStateMachine)
        {
            _position = postion;
        }

        #endregion

        #region Events

        public override void Enter()
        {
            PlayerStateMachine.transform.position = _position;
        }

        public override void Tick(float deltaTime)
        {
        }
        
        public override void TickFixed(float deltaTime)
        {
            if (PlayerStateMachine.transform.position != _position)
                PlayerStateMachine.transform.position = _position;
            else
                PlayerStateMachine.SwitchState(new PlayerIdleState(PlayerStateMachine));
        }

        public override void Exit()
        {
        }

        #endregion
    }
}