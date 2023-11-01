using Game.Scripts._Data.Observer;
using Game.Scripts._Data.SceneData;
using Game.Scripts.Generic.Managers;
using Game.Scripts.Generic.PlayerPref;
using UnityEngine;

namespace Game.Scripts._Observers.LevelLoaderObserver
{
    public class LevelLoaderObserverEvent : Observer
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
            if (data is not SceneData sceneData) return;

            if (sceneData.name != "Menu")
            {
                PlayerPrefsSave.SetPlayerPositionMenu(transform.position + Vector3.up);
            }
            
            ScenesManager.LoadScene(sceneData);
        }

        #endregion
    }
}
