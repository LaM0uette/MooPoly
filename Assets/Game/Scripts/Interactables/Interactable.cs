using EPOOutline;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Scripts.Interactables
{
    public abstract class Interactable : MonoBehaviour
    {
        #region Statements

        [Space, Title("Outline")]
        [SerializeField] protected Outlinable Outline;

        #endregion
        
        #region Functions
        
        public Transform GetTransform() => gameObject.transform;

        public abstract void Interact();
        public abstract void ShowOutline(bool value);

        public virtual void Destroy() { }

        #endregion
    }
}
