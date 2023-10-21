using Game.Scripts.BaseStateMachine;

namespace Game.Scripts.Player.PlayerStateMachine
{
    public abstract class PlayerBaseState : State
    {
        #region Statements

        protected readonly PlayerStateMachine PlayerStateMachine;
        
        protected PlayerBaseState(PlayerStateMachine playerStateMachine)
        {
            PlayerStateMachine = playerStateMachine;
        }

        #endregion
    }
}
