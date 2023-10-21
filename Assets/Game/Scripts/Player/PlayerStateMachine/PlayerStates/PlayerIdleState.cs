namespace Game.Scripts.Player.PlayerStateMachine.PlayerStates
{
    public class PlayerIdleState : PlayerBaseState
    {
        #region Statements

        public PlayerIdleState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
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
            
            PlayerStateMachine.TransitionToAnimation(PlayerStateMachine.IdleStateHash, .2f);
        }

        public override void Tick(float deltaTime)
        {
        }

        public override void TickLate(float deltaTime)
        {
            CameraZoom();
        }
        
        public override void CheckState()
        {
            if (PlayerStateMachine.IsMoving())
                PlayerStateMachine.SwitchState(new PlayerMoveState(PlayerStateMachine));
        }

        public override void Exit()
        {
            UnsubscribeEvents();
        }

        #endregion
    }
}