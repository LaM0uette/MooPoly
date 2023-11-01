using Game.Scripts.BaseStateMachine;
using UnityEngine;

namespace Game.Scripts.Enemies.EnemyStateMachine
{
    public abstract class EnemyBaseState : State
    {
        #region Statements

        protected readonly EnemyStateMachine EnemyStateMachine;
        
        protected EnemyBaseState(EnemyStateMachine enemyStateMachine)
        {
            EnemyStateMachine = enemyStateMachine;
        }

        #endregion
        
        #region Animations

        protected void AnimatorSetFloat(int id, float value, float dampTime = .1f)
        {
            EnemyStateMachine.Animator.SetFloat(id, value, dampTime, Time.deltaTime);
        }
        
        protected bool HasAnimationReachedStage(float value, int layerIndex = 0)
        {
            var state = EnemyStateMachine.Animator.GetCurrentAnimatorStateInfo(layerIndex);
            var normalizedTime = Mathf.Repeat(state.normalizedTime,1f);

            return normalizedTime > value;
        }

        #endregion

        #region Functions

        protected void Move(float speed)
        {
            if (EnemyStateMachine.EnemyPath is null) return;
            
            var distanceToMove = speed / 10 * Time.deltaTime;
            var percentageToMove = distanceToMove / EnemyStateMachine.TotalSplineLength;

            EnemyStateMachine.PercentageOfCurve += percentageToMove;
            EnemyStateMachine.PercentageOfCurve = Mathf.Clamp01(EnemyStateMachine.PercentageOfCurve);

            var position = EnemyStateMachine.EnemyPath.EvaluatePosition(EnemyStateMachine.PercentageOfCurve);
            position.y += EnemyStateMachine.Enemy.HeightOffset;
            EnemyStateMachine.transform.position = position;

            var direction = EnemyStateMachine.EnemyPath.EvaluateTangent(EnemyStateMachine.PercentageOfCurve);
            EnemyStateMachine.transform.forward = direction;
        }

        #endregion
    }
}
