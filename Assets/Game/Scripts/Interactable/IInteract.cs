using UnityEngine;

namespace Game.Scripts.Interactable
{
    public interface IInteract
    {
        public void Interact();
        public void ShowOutline(bool value);
        public void ShowUi();
        public Transform GetTransform();
    }
}
