using System.Collections;
using Game.Scripts.BaseStateMachine;
using Game.Scripts.Enemies.EnemyStateMachine.EnemyStates;
using Game.Scripts.ScriptableObjects.EnemyData;
using UnityEngine;
using UnityEngine.Splines;

namespace Game.Scripts.Enemies.EnemyStateMachine
{
    [RequireComponent(typeof(Animator))]
    public class EnemyStateMachine : StateMachine
    {
        #region AnimationHash
        
        // States
        public static readonly int MoveStateHash = Animator.StringToHash("MoveBlendTree");
        public static readonly int AttackHash = Animator.StringToHash("Attack");
        public static readonly int DieHash = Animator.StringToHash("Die");
            
        // Variables
        public static readonly int SpeedHash = Animator.StringToHash("Speed");
        
        #endregion
        
        #region Statements

        // TEMP: Temporary
        public SplineContainer WalkPath;
        public float PercentageOfCurve;
        public float TotalSplineLength;
        
        public Animator Animator { get; private set; }
        public bool IsTransitioning { get; private set; }

        public EnemyData EnemyData;

        private void Awake()
        {
            Animator = GetComponent<Animator>();
        }

        private void Start()
        {
            PercentageOfCurve = 0;
            TotalSplineLength = WalkPath.CalculateLength();
            
            SwitchState(new EnemyMoveState(this));
        }

        #endregion
        
        #region Animations

        public void TransitionToAnimation(int animationId, float transitionDuration = .1f)
        {
            Animator.CrossFadeInFixedTime(animationId, transitionDuration);
            IsTransitioning = true;
            
            StartCoroutine(EndTransitionAfterDelay(transitionDuration));
        }
        
        private IEnumerator EndTransitionAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            IsTransitioning = false;
        }

        #endregion

        #region Functions

        public void Dead()
        {
            Destroy(gameObject);
        }

        #endregion
    }
}
