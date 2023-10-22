using UnityEngine;

namespace Game.Scripts.Player.PlayerStateMachine.PlayerStates
{
    public class PlayerMapState : PlayerBaseState
    {
        #region Statements

        public PlayerMapState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
        {
        }

        #endregion
        
        #region Subscribe/Unsubscribe Events

        private void SubscribeEvents()
        {
            PlayerStateMachine.Inputs.LevelMapEvent += LevelMapClose;
        }
        
        private void UnsubscribeEvents()
        {
            PlayerStateMachine.Inputs.LevelMapEvent -= LevelMapClose;
        }

        #endregion

        #region Events

        public override void Enter()
        {
            SubscribeEvents();
            
            PlayerStateMachine.TransitionToAnimation(PlayerStateMachine.IdleStateHash, .2f);
            AnimatorSetFloat(PlayerStateMachine.SpeedHash, 0, 0.1f);
            
            PlayerStateMachine.TopDownCamera.m_Priority = 10;
            PlayerStateMachine.MapCamera.m_Priority = 11;
        }

        public override void Tick(float deltaTime)
        {
        }

        public override void CheckState()
        {
        }

        public override void Exit()
        {
            UnsubscribeEvents();
            
            PlayerStateMachine.TopDownCamera.m_Priority = 11;
            PlayerStateMachine.MapCamera.m_Priority = 10;
        }

        #endregion

        #region Functions

        private void LevelMapClose()
        {
            PlayerStateMachine.SwitchState(new PlayerIdleState(PlayerStateMachine));
        }

        #endregion
    }
}