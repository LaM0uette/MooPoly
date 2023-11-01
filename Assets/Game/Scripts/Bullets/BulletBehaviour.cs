using Game.Scripts.Bullets.BulletFactory;
using Game.Scripts.Enemies.EnemyStateMachine;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Scripts.Bullets
{
    public class BulletBehaviour : MonoBehaviour
    {
        #region Statements

        [CanBeNull] private EnemyStateMachine _enemy { get; set; }
        private Bullet Bullet { get; } = new();
        
        private float _turretDamage { get; set; }

        public void Init(EnemyStateMachine enemy, float turretDamage)
        {
            _enemy = enemy;
            _turretDamage = turretDamage;
        }
        
        #endregion

        #region Events

        private void FixedUpdate()
        {
            if (_enemy is null || _enemy.Enemy.IsDead)
            {
                Destroy(gameObject);
                return;
            }
            
            FollowEnemy();
        }

        #endregion

        #region Functions
        
        private void FollowEnemy()
        {
            if (_enemy is null || _enemy.Enemy.IsDead) return;

            var direction = _enemy.Target.transform.position - transform.position;

            transform.rotation = Quaternion.LookRotation(direction);

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
            if (_enemy is null || _enemy.Enemy.IsDead) return;
            
            _enemy.TakeDamage(_turretDamage + Bullet.Damage);
            Destroy(gameObject);
        }

        #endregion
    }
}
