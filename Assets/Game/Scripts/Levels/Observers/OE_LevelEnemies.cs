using Game.Scripts._Data.Observer;
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

        public override void OnNotify<T>(T isAlive)
        {
            if (isAlive is not bool alive) return;
            
            if (alive)
                _levelManager.EnemySpawned();
            else
                _levelManager.EnemyDied();
            
            _levelManager.CheckEnemiesAlive();
        }

        #endregion
    }
}
