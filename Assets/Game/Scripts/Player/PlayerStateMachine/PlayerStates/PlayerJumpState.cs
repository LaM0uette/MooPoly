using UnityEngine;

namespace Game.Scripts.Player.PlayerStateMachine.PlayerStates
{
    public class PlayerJumpState : PlayerBaseState
    {
        #region Statements

        public PlayerJumpState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
        {
        }

        #endregion

        #region Events

        public override void Enter()
        {
            PlayerStateMachine.Velocity = new Vector3(PlayerStateMachine.Velocity.x, PlayerStateMachine.JumpForce, PlayerStateMachine.Velocity.z);
            PlayerStateMachine.TransitionToAnimation(PlayerStateMachine.JumpHash);
        }

        public override void CheckState()
        {
            if (PlayerStateMachine.IsTransitioning) return;
            
            if (HasAnimationReachedStage(.2f) && PlayerStateMachine.IsGrounded())
                PlayerStateMachine.SwitchState(new PlayerMoveState(PlayerStateMachine));
        }

        public override void Tick(float deltaTime)
        {
            ApplyGravity(2.4f);
            
            Move(PlayerStateMachine.MoveSpeed);
        }

        public override void Exit()
        {
        }

        #endregion
    }
}