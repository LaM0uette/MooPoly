using System.Collections;
using Game.Scripts.BaseStateMachine;
using Game.Scripts.Enemies.EnemyFactory;
using Game.Scripts.Enemies.EnemyStateMachine.EnemyStates;
using Game.Scripts.Observers;
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
        public bool IsDead { get; set; }

        // Splines
        [CanBeNull] public SplineContainer EnemyPath { get; set; }
        public float PercentageOfCurve { get; set; }
        public float TotalSplineLength { get; private set; }
        
        // Target
        public GameObject Target;

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
            IsDead = false;
            _observer.Notify(true);
        }
        
        private void OnDisable()
        {
            IsDead = true;
            _observer.Notify(false);
        }
        
        #endregion
        
        #region Animations

        public void TransitionToAnimation(int animationId, float transitionDuration = .1f)
        {
            Animator.CrossFadeInFixedTime(animationId, transitionDuration);
            
            StartCoroutine(EndTransitionAfterDelay(transitionDuration));
        }
        
        private static IEnumerator EndTransitionAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
        }

        #endregion

        #region Functions
        
        public void TakeDamage(float damage)
        {
            Enemy.Health -= damage;
            CheckIfDead();
        }
        
        private void CheckIfDead()
        {
            if (Enemy.Health > 0) return;
            
            Dead();
            //SwitchState(new EnemyDieState(this));
        }

        public void Dead()
        {
            Destroy(gameObject);
        }

        #endregion
    }
}
