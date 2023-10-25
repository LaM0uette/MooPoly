using Game.Scripts.Observers;
using Game.Scripts.Player.Controller;
using Game.Scripts.Scenes;
using Game.Scripts.ScriptableObjects.SceneData;
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
            if (data is not SceneData sceneData) return;

            PlayerController.RemoveCurrentInteract();
            ScenesManager.LoadScene(sceneData);
        }

        #endregion
    }
}
