using Game.Scripts._Data.Observer;
using Game.Scripts.Generic.Managers;
using Game.Scripts.Ui.GameHUD.Screens;
using UnityEngine;

namespace Game.Scripts._Observers.MooCoinObserver
{
    public class MooCoinObserverEvent : Observer
    {
        #region Statements

        [SerializeField] private ObserverEvent _observer;
        private UIS_LevelCoins _uisLevelCoins;

        private void Awake()
        {
            _uisLevelCoins = GetComponent<UIS_LevelCoins>();
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

        public override void OnNotify<T>(T data)
        {
            if (data is not int coins) return;

            GameManager.Instance.EarnLevelMooCoins(coins);
            _uisLevelCoins.UpdateLevelCoins(GameManager.Instance.CurrentLevelMooCoins);
        }

        #endregion
    }
}
