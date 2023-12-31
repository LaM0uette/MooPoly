﻿namespace Game.Scripts.Enemies.EnemyStateMachine.EnemyStates
{
    public class EnemyMoveState : EnemyBaseState
    {
        #region Statements

        public EnemyMoveState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
        {
        }

        #endregion

        #region Events

        public override void Enter()
        {
            EnemyStateMachine.TransitionToAnimation(EnemyStateMachine.MoveStateHash, .2f);
        }

        public override void Tick(float deltaTime)
        {
            EnemyStateMachine.CheckHealth();
            
            if (EnemyStateMachine.EnemyPath is null)
            {
                AnimatorSetFloat(EnemyStateMachine.SpeedHash, 0);
                return;
            }
            
            AnimatorSetFloat(EnemyStateMachine.SpeedHash, 1);
            
            if (EnemyStateMachine.PercentageOfCurve >= 1)
            {
                EnemyStateMachine.Steal();
                return;
            }
            
            Move(EnemyStateMachine.Enemy.MoveSpeed);
        }

        public override void Exit()
        {
        }

        #endregion
    }
}