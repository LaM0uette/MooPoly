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
        private float _turretDamage { get; set; }

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

        public void Init(EnemyStateMachine enemy, float turretDamage)
        {
            _enemy = enemy;
            _turretDamage = turretDamage;
        }

        private void FollowEnemy()
        {
            if (_enemy is null || _enemy.IsDead) return;
            
            var direction = _enemy.Target.transform.position - transform.position;
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
            if (_enemy is null || _enemy.IsDead) return;
            
            _enemy.TakeDamage(_turretDamage + Bullet.Damage);
            Destroy(gameObject);
        }

        #endregion
    }
}
