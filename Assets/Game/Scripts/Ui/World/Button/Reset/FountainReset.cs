using EPOOutline;
using Game.Scripts.Generic.PlayerPref;
using Game.Scripts.Interactable;
using UnityEngine;

namespace Game.Scripts.Ui.World.Button.Reset
{
    public class FountainReset : MonoBehaviour, IInteract
    {
        #region Statements

        private Outlinable _outlinable;

        private void Awake()
        {
            _outlinable = GetComponent<Outlinable>();
        }

        #endregion
        
        #region Functions

        public void Interact()
        {
            PlayerPrefsSave.DeletePlayerPositionMenu();
        }
        
        public void ShowOutline(bool value)
        {
            _outlinable.enabled = value;
        }

        public void ShowUi()
        {
        }

        public Transform GetTransform() => transform;

        #endregion
    }
}
