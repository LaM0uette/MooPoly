using Game.Scripts.Observers;
using UnityEngine;

namespace Game.Scripts.MooCoins
{
    public class OE_MooCoins : Observer
    {
        #region Statements

        [SerializeField] private ObserverEvent _observer;

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
            if (data is not MooCoin mooCoin) return;
            
            Debug.Log($"MooCoin: {mooCoin.CandyEarned}");
        }

        #endregion
    }
}
