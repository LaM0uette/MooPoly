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
        
        protected float AnimatorGetFloat(int id)
        {
            return EnemyStateMachine.Animator.GetFloat(id);
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
            // TODO: Move enemy
        }

        #endregion
    }
}
