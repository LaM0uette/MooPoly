using Game.Scripts.Generic.PlayerPref;
using Game.Scripts.Interactable;
using UnityEngine;

namespace Game.Scripts.Ui.World.Button.Reset
{
    public class FountainReset : MonoBehaviour, IInteract
    {
        public void Interact()
        {
            PlayerPrefsSave.DeletePlayerPositionMenu();
        }

        public Transform GetTransform() => transform;
    }
}
