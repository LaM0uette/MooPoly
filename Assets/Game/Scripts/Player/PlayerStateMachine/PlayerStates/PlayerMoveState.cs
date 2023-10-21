namespace Game.Scripts.Player.PlayerStateMachine.PlayerStates
{
    public class PlayerMoveState : PlayerBaseState
    {
        #region Statements

        public PlayerMoveState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
        {
        }

        #endregion

        #region Subscribe/Unsubscribe Events

        private void SubscribeEvents()
        {
            PlayerStateMachine.Inputs.JumpEvent += Jump;
        }
        
        private void UnsubscribeEvents()
        {
            PlayerStateMachine.Inputs.JumpEvent -= Jump;
        }

        #endregion
        
        #region Events

        public override void Enter()
        {
            SubscribeEvents();
            
            PlayerStateMachine.TransitionToAnimation(PlayerStateMachine.MoveStateHash);
        }

        public override void Tick(float deltaTime)
        {
            ApplyGravity();
            
            Move(PlayerStateMachine.MoveSpeed);
            
            var moveSpeed = PlayerStateMachine.IsMoving() ? 1 : 0;
            var dampTime = AnimatorGetFloat(PlayerStateMachine.SpeedHash) > 0.01f ? 0.1f : 0;
            AnimatorSetFloat(PlayerStateMachine.SpeedHash, moveSpeed, dampTime);
        }

        public override void TickLate(float deltaTime)
        {
            CameraZoom();
        }

        public override void CheckState()
        {
            if (PlayerStateMachine.IsTransitioning) return;
            
            if (!PlayerStateMachine.IsMoving() && AnimatorGetFloat(PlayerStateMachine.SpeedHash) <= 0.01f)
                PlayerStateMachine.SwitchState(new PlayerIdleState(PlayerStateMachine));
        }

        public override void Exit()
        {
            UnsubscribeEvents();
            ResetVelocity();
        }

        #endregion
    }
}