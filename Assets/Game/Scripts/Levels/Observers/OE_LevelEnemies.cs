using Game.Scripts.Enemies.EnemyFactory;
using Game.Scripts.Observers;
using UnityEngine;

namespace Game.Scripts.Levels.Observers
{
    public class OE_LevelEnemies : Observer
    {
        #region Statements

        [SerializeField] private ObserverEvent _observer;
        [SerializeField] private LevelManager _levelManager;

        #endregion

        #region Events

        private void OnEnable()
        {
            _observer.Register(this);
        }

        private void OnDisable()
        {
            _observer.Unregister(this);
        }

        public override void OnNotify<T>(T enemyT)
        {
            if (enemyT is not Enemy enemy) return;
            
            if (!enemy.IsDead)
                _levelManager.EnemySpawned();
            else
                _levelManager.EnemyDied(enemy);
            
            _levelManager.CheckEnemiesAlive();
        }

        #endregion
    }
}
