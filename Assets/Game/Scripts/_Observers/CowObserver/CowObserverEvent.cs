using System;
using Game.Scripts._Data.Observer;
using Game.Scripts.Farm;
using UnityEngine;

namespace Game.Scripts._Observers.CowObserver
{
    public class CowObserverEvent : Observer
    {
        #region Statements

        [SerializeField] private ObserverEvent _observer;
        private FarmHandler _farmHandler;

        private void Awake()
        {
            _farmHandler = GetComponent<FarmHandler>();
        }

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

        public override void OnNotify()
        {
            _farmHandler.KillCow();
        }

        #endregion
    }
}
