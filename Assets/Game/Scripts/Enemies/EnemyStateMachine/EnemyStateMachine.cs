using System.Collections;
using Game.Scripts.BaseStateMachine;
using Game.Scripts.Enemies.EnemyFactory;
using Game.Scripts.Enemies.EnemyStateMachine.EnemyStates;
using Game.Scripts.Observers;
using Game.Scripts.ScriptableObjects.EnemyData;
using JetBrains.Annotations;
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
        
        // Components
        public Animator Animator { get; private set; }
        public Enemy Enemy { get; set; } = new();
        
        // States
        public bool IsTransitioning { get; private set; }

        // Splines
        [CanBeNull] public SplineContainer EnemyPath { get; set; }
        public float PercentageOfCurve { get; set; }
        public float TotalSplineLength { get; private set; }

        [SerializeField] private ObserverEvent _observer;
        
        private void Awake()
        {
            Animator = GetComponent<Animator>();
        }

        private void Start()
        {
            PercentageOfCurve = 0;
            
            if (EnemyPath is not null) 
                TotalSplineLength = EnemyPath.CalculateLength();

            SwitchState(new EnemyMoveState(this));
        }

        #endregion
        
        #region Events
        
        private void OnEnable()
        {
            _observer.Notify(true);
        }
        
        private void OnDisable()
        {
            _observer.Notify(false);
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
