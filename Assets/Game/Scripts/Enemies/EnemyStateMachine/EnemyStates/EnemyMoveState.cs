using UnityEngine;

namespace Game.Scripts.Enemies.EnemyStateMachine.EnemyStates
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
            if (EnemyStateMachine.WalkPath is null) return;
            
            if (EnemyStateMachine.PercentageOfCurve >= 1)
            {
                Debug.Log("FIN !");
                EnemyStateMachine.Dead();
                return;
            }
            
            Move(EnemyStateMachine.EnemyData.MoveSpeed);
            AnimatorSetFloat(EnemyStateMachine.SpeedHash, 1);
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