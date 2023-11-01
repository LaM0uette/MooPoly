using System.Collections;
using Game.Scripts._Data.Observer;
using Game.Scripts.BaseStateMachine;
using Game.Scripts.Enemies.EnemyFactory;
using Game.Scripts.Enemies.EnemyStateMachine.EnemyStates;
using Game.Scripts.MooCoins;
using Game.Scripts.MooCoins.MooCoinFactory;
using Game.Scripts.StaticUtilities;
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

        public GameObject Target;

        [SerializeField] private ObserverEvent _observer;
        
        private GameObject _mooCoinsParent;
        
        private void Awake()
        {
            Animator = GetComponent<Animator>();
            _mooCoinsParent = GameObject.FindGameObjectWithTag(TagRef.MooCoinsParent);
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
            Enemy.IsDead = false;
            _observer.Notify("Spawned");
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
        
        public void TakeDamage(float damage)
        {
            Enemy.Health -= damage;
        }
        
        public void CheckHealth()
        {
            if (Enemy.Health > 0) return;
            
            Die();
        }
        
        public void EarnMooCoins()
        {
            var mooCoins = Enemy.MooCoins;
            var pos = transform.position;
            pos.y += 1f;
            
            foreach (var mooCoin in mooCoins)
            {
                var random = Random.Range(0, 1f);
                if (random > mooCoin.Probability) continue;
                
                var mooCoinGameObject = Instantiate(mooCoin.MooCoinData.Prefab, pos, Quaternion.Euler(90, 0, Random.Range(0, 359f)), _mooCoinsParent.transform);
                var mooCoinBehaviour = mooCoinGameObject.GetComponent<MooCoinBehaviour>();
                mooCoinBehaviour.MooCoin = MooCoinFactory.Create(mooCoin.MooCoinData);
                
                var randomForce = new Vector3(Random.Range(-0.3f, 0.3f), Random.Range(1, 3), Random.Range(-0.3f, 0.3f));
                mooCoinBehaviour.Rigidbody.AddForce(randomForce * 5f, ForceMode.Impulse);
            }
        }
        
        public bool IsDead() => Enemy.IsDead;
        public void Steal() => SwitchState(new EnemyStealState(this));
        public void Die() => SwitchState(new EnemyDieState(this));

        public void StealDead()
        {
            Enemy.IsDead = true;
            _observer.Notify("ReachedEnd");
            
            Destroy(gameObject);
        }
        
        public void Dead()
        {
            Enemy.IsDead = true;
            _observer.Notify("Died");
            
            Destroy(gameObject);
        }

        #endregion
    }
}
