using Game.Scripts.Turrets.TurretUpgrader;
using UnityEngine;

namespace Game.Scripts.Player.PlayerStateMachine.PlayerStates
{
    public class PlayerUpgradeState : PlayerBaseState
    {
        #region Statements

        private TurretUpgrader _turretUpgrader;
        
        public PlayerUpgradeState(PlayerStateMachine playerStateMachine, TurretUpgrader turretUpgrader) : base(playerStateMachine)
        {
            _turretUpgrader = turretUpgrader;
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
            Debug.Log("Enter PlayerUpgradeState");
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

            Debug.Log(_turretUpgrader.TurretStateMachine.Turret.Damage);
        }

        public override void TickLate(float deltaTime)
        {
            CameraZoom();
        }

        public override void Exit()
        {
            UnsubscribeEvents();
            Debug.Log("Exit PlayerUpgradeState");
        }

        #endregion
    }
}