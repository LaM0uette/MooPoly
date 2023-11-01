using Game.Scripts._Data.Observer;
using Game.Scripts.Levels;
using UnityEngine;

namespace Game.Scripts._Observers.EnemyObserver
{
    public class EnemyObserverEvent : Observer
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

        public override void OnNotify<T>(T evt)
        {
            if (evt is not string eventName) return;
            
            if (eventName == "Spawned")
                _levelManager.EnemySpawned();
            else if (eventName == "Died")
                _levelManager.EnemyDied();
            else if (eventName == "ReachedEnd")
                _levelManager.EnemyReachedEnd();
            
            _levelManager.CheckEnemiesAlive();
        }

        #endregion
    }
}
