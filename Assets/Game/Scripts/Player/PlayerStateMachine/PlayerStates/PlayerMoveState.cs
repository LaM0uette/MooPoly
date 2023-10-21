namespace Game.Scripts.Player.PlayerStateMachine.PlayerStates
{
    public class PlayerMoveState : PlayerBaseState
    {
        #region Statements

        public PlayerMoveState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
        {
        }

        #endregion

        #region Events

        public override void Enter()
        {
            PlayerStateMachine.TransitionToAnimation(PlayerStateMachine.MoveStateHash);
        }

        public override void Tick(float deltaTime)
        {
            ApplyGravity();
            
            var moveSpeed = PlayerStateMachine.IsMoving() ? 1 : 0;
            
            Move(PlayerStateMachine.MoveSpeed);
            AnimatorSetFloat(PlayerStateMachine.SpeedHash, moveSpeed, 0.1f);
        }

        public override void TickLate(float deltaTime)
        {
        }

        public override void CheckState()
        {
            if (PlayerStateMachine.IsTransitioning) return;
            
            if (!PlayerStateMachine.IsMoving() && AnimatorGetFloat(PlayerStateMachine.SpeedHash) <= 0.01f)
                PlayerStateMachine.SwitchState(new PlayerIdleState(PlayerStateMachine));
        }

        public override void Exit()
        {
            ResetVelocity();
        }

        #endregion
    }
}