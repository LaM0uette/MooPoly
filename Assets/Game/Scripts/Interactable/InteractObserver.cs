using Game.Scripts.Observers;
using UnityEngine;

namespace Game.Scripts.Interactable
{
    public class InteractObserver : MonoBehaviour, IObserver
    {
        #region Statements

        [SerializeField] private Observer _observer;

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

        public void OnNotify()
        {
            Debug.Log("Interact");
        }

        #endregion
    }
}
