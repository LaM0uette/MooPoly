using Game.Scripts.Observers;
using UnityEngine;

namespace Game.Scripts.Ui.World.Button.Level
{
    public class OE_ButtonLevel : Observer
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
            if (data is not ButtonLevel buttonLevel) return;
            
            Debug.Log("OnNotify: " + buttonLevel.IsUnlocked);
        }

        #endregion
    }
}
