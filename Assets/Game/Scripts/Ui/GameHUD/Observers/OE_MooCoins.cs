using System;
using Game.Scripts.Generic.Managers;
using Game.Scripts.MooCoins;
using Game.Scripts.Observers;
using Game.Scripts.Ui.GameHUD.Screens;
using UnityEngine;

namespace Game.Scripts.Ui.GameHUD.Observers
{
    public class OE_MooCoins : Observer
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
            var coinsEarned = data switch
            {
                MooCoin mooCoin => mooCoin.CandyEarned,
                int coins => coins,
                _ => 0
            };

            GameManager.Instance.EarnLevelMooCoins(coinsEarned);
            _uisLevelCoins.UpdateLevelCoins(GameManager.Instance.CurrentLevelMooCoins);
        }

        #endregion
    }
}
