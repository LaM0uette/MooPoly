using UnityEngine;

namespace Game.Scripts.Player.PlayerStateMachine.PlayerStates
{
    public class PlayerKnockbackState : PlayerBaseState
    {
        #region Statements
        
        private Vector3 _knockbackDirection;

        public PlayerKnockbackState(PlayerStateMachine playerStateMachine, Vector3 knockbackDirection) : base(playerStateMachine)
        {
            _knockbackDirection = knockbackDirection;
        }

        #endregion
        
        #region Events

        public override void Enter()
        {
            PlayerStateMachine.Velocity = _knockbackDirection * 60f + Vector3.up * 10f;
            PlayerStateMachine.TransitionToAnimation(PlayerStateMachine.KnockbackHash);
        }

        public override void CheckState()
        {
            if (PlayerStateMachine.IsTransitioning) return;
    
            if (HasAnimationReachedStage(.6f))
                PlayerStateMachine.SwitchState(new PlayerIdleState(PlayerStateMachine));
        }

        public override void Tick(float deltaTime)
        {
            ApplyGravity();
            Move(_knockbackDirection);
        }

        public override void Exit()
        {
            ResetVelocity();
        }

        #endregion

        #region Functions

        private void Move(Vector3 direction)
        {
            PlayerStateMachine.Controller.Move(direction * Time.deltaTime);
        }

        #endregion
    }
}