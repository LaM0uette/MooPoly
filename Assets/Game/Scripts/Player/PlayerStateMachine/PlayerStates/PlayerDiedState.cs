using UnityEngine;

namespace Game.Scripts.Player.PlayerStateMachine.PlayerStates
{
    public class PlayerDiedState : PlayerBaseState
    {
        #region Statements

        public PlayerDiedState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
        {
        }

        #endregion

        #region Events

        public override void Enter()
        {
            PlayerStateMachine.transform.position = Vector3.zero;
        }

        public override void Tick(float deltaTime)
        {
        }
        
        public override void TickFixed(float deltaTime)
        {
            if (PlayerStateMachine.transform.position != Vector3.zero)
                PlayerStateMachine.transform.position = Vector3.zero;
            else
                PlayerStateMachine.SwitchState(new PlayerIdleState(PlayerStateMachine));
        }

        public override void Exit()
        {
        }

        #endregion
    }
}