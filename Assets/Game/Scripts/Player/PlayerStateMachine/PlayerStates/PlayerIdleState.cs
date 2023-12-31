﻿using Game.Scripts.Player.Controller;
using Game.Scripts.Turrets.TurretUpgrader;
using UnityEngine;

namespace Game.Scripts.Player.PlayerStateMachine.PlayerStates
{
    public class PlayerIdleState : PlayerBaseState
    {
        #region Statements
        
        private float _inactivityTime;
        private TurretUpgrader _currentTurretUpgrader;

        public PlayerIdleState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
        {
        }

        #endregion
        
        #region Subscribe/Unsubscribe Events

        private void SubscribeEvents()
        {
            PlayerStateMachine.Inputs.JumpEvent += Jump;
            PlayerStateMachine.Inputs.InteractEvent += Interact;
            PlayerStateMachine.Inputs.LevelMapEvent += LevelMap;
            
            PlayerController.OnSendTurretUpgrader += SendTurretUpgrader;
        }
        
        private void UnsubscribeEvents()
        {
            PlayerStateMachine.Inputs.JumpEvent -= Jump;
            PlayerStateMachine.Inputs.InteractEvent -= Interact;
            PlayerStateMachine.Inputs.LevelMapEvent -= LevelMap;
            
            PlayerController.OnSendTurretUpgrader += SendTurretUpgrader;
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
            
            if (_currentTurretUpgrader is not null)
                PlayerStateMachine.SwitchState(new PlayerUpgradeState(PlayerStateMachine, _currentTurretUpgrader));
        }

        public override void Tick(float deltaTime)
        {
            ApplyGravity();
            Move();
            
            if (!HasAnimationReachedStage(.95f)) 
            {
                _inactivityTime += deltaTime;
                return;
            }

            TransitionToRandomIdleAnimation();
        }

        public override void TickLate(float deltaTime)
        {
            CameraZoom();
        }

        public override void Exit()
        {
            UnsubscribeEvents();
            _inactivityTime = 0;
        }

        #endregion

        #region Functions

        private void TransitionToRandomIdleAnimation()
        {
            if (_inactivityTime > 12.0f)
            {
                var randomAnimation = Random.Range(1, 6);
                AnimatorSetInt(PlayerStateMachine.IdleBlendHash, randomAnimation);
                _inactivityTime = 0;
            }
            else
            {
                AnimatorSetInt(PlayerStateMachine.IdleBlendHash, 0);
            }
        }

        private void SendTurretUpgrader(TurretUpgrader turretUpgrader)
        {
            _currentTurretUpgrader = turretUpgrader;
        }

        #endregion
    }
}