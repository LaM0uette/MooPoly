using Game.Scripts._Data.Observer;
using Game.Scripts._Data.SceneData;
using Game.Scripts.Generic.PlayerPref;
using Game.Scripts.Player.Controller;
using Game.Scripts.Scenes;
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

            if (sceneData.name != "Menu")
                PlayerPrefsSave.SetPlayerPositionMenu(transform.position + Vector3.up);
            
            PlayerController.RemoveCurrentInteract();
            ScenesManager.LoadScene(sceneData);
        }

        #endregion
    }
}
