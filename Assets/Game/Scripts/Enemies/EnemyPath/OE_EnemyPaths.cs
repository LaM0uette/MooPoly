using Game.Scripts.Levels;
using Game.Scripts.Observers;
using UnityEngine;
using UnityEngine.Splines;

namespace Game.Scripts.Enemies.EnemyPath
{
    public class OE_EnemyPaths : Observer
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

        public override void OnNotify<T>(T data)
        {
            if (data is not SplineContainer splineContainer) return;
            
            _levelManager.EnemyPaths.Add(splineContainer);
        }

        #endregion
    }
}
