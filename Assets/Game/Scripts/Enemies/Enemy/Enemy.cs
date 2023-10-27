using Game.Scripts.Observers;
using UnityEngine;

namespace Game.Scripts.Enemies.Enemy
{
    public class Enemy : MonoBehaviour
    {
        #region Statements

        [SerializeField] private ObserverEvent _observer;

        #endregion

        #region Events

        private void OnEnable()
        {
            _observer.Notify(true);
        }
        
        private void OnDisable()
        {
            _observer.Notify(false);
        }

        #endregion
    }
}
