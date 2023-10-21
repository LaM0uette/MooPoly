namespace Game.Scripts.Player.PlayerStateMachine.PlayerStates
{
    public class PlayerIdleState : PlayerBaseState
    {
        #region Statements

        public PlayerIdleState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
        {
        }

        #endregion

        #region Events

        public override void Enter()
        {
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
        }

        #endregion
    }
}