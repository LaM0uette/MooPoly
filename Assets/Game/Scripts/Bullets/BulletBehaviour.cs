using Game.Scripts.Enemies.EnemyStateMachine;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Scripts.Bullets
{
    public class BulletBehaviour : MonoBehaviour
    {
        #region Statements

        public Bullet Bullet { get; set; } = new();
        [CanBeNull] private EnemyStateMachine _enemy { get; set; }

        #endregion

        #region Events

        private void FixedUpdate()
        {
            if (_enemy is null || _enemy.IsDead)
            {
                Destroy(gameObject);
                return;
            }
            
            FollowEnemy();
        }

        #endregion

        #region Functions
        
        public void SetEnemy(EnemyStateMachine enemy) => _enemy = enemy;

        private void FollowEnemy()
        {
            if (_enemy is null || _enemy.IsDead) return;
            
            var direction = _enemy.transform.position - transform.position;
            var distanceThisFrame = Bullet.Speed * Time.deltaTime;
            
            if (direction.magnitude <= distanceThisFrame)
            {
                HitTarget();
                return;
            }
            
            transform.Translate(direction.normalized * distanceThisFrame, Space.World);
        }
        
        private void HitTarget()
        {
            Debug.Log("Hit target");
            Destroy(gameObject);
        }

        #endregion
    }
}
