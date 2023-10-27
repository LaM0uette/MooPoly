using System.Collections.Generic;
using Game.Scripts.Observers;
using UnityEngine;
using UnityEngine.Splines;

namespace Game.Scripts.Enemies.EnemyPath
{
    public class OE_EnemyPaths : Observer
    {
        #region Statements

        [SerializeField] private ObserverEvent _observer;

        public List<SplineContainer> EnemyPaths;

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
            
            EnemyPaths.Add(splineContainer);
        }

        #endregion
    }
}
