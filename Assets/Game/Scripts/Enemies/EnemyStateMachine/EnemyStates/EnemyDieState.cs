using UnityEngine;

namespace Game.Scripts.Enemies.EnemyStateMachine.EnemyStates
{
    public class EnemyDieState : EnemyBaseState
    {
        #region Statements

        public EnemyDieState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
        {
        }

        #endregion

        #region Events

        public override void Enter()
        {
            EnemyStateMachine.TransitionToAnimation(EnemyStateMachine.DieHash);
        }

        public override void Tick(float deltaTime)
        {
            if (EnemyStateMachine.IsTransitioning || !HasAnimationReachedStage(.9f)) return;

            Debug.Log("Dead");
            EnemyStateMachine.Dead();
        }

        public override void Exit()
        {
        }

        #endregion
    }
}