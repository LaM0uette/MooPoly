using UnityEngine;

namespace Game.Scripts.Interactable
{
    public interface IInteract
    {
        public void Interact();
        public Transform GetTransform();
    }
}
