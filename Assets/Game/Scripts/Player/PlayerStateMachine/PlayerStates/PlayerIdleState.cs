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
            PlayerStateMachine.TransitionToAnimation(PlayerStateMachine.IdleState, .2f);
        }

        public override void Tick(float deltaTime)
        {
        }
        
        public override void CheckState()
        {
        }

        public override void Exit()
        {
        }

        #endregion
    }
}