namespace Game.Scripts.Player.PlayerStateMachine.PlayerStates
{
    public class PlayerUpgradeState : PlayerBaseState
    {
        #region Statements

        public PlayerUpgradeState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
        {
        }

        #endregion

        #region Subscribe/Unsubscribe Events

        private void SubscribeEvents()
        {
            PlayerStateMachine.Inputs.InteractEvent += Interact;
            PlayerStateMachine.Inputs.LevelMapEvent += LevelMap;
        }
        
        private void UnsubscribeEvents()
        {
            PlayerStateMachine.Inputs.InteractEvent -= Interact;
            PlayerStateMachine.Inputs.LevelMapEvent -= LevelMap;
        }

        #endregion
        
        #region Events

        public override void Enter()
        {
            SubscribeEvents();
            
            PlayerStateMachine.TransitionToAnimation(PlayerStateMachine.IdleStateHash, .2f);
        }

        public override void CheckState()
        {
            if (PlayerStateMachine.IsMoving())
                PlayerStateMachine.SwitchState(new PlayerMoveState(PlayerStateMachine));
        }

        public override void Tick(float deltaTime)
        {
            ApplyGravity();
            Move();
        }

        public override void TickLate(float deltaTime)
        {
            CameraZoom();
        }

        public override void Exit()
        {
            UnsubscribeEvents();
        }

        #endregion
    }
}