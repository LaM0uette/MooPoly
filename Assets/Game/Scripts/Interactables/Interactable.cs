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
        
        #region virtual
        
        public abstract void Interact();

        public virtual void Enter()
        {
            if (Outline is null || Outline.isActiveAndEnabled) return;
            SetOutline(true);
        }
        
        public virtual void Exit()
        {
            if (Outline is null || !Outline.isActiveAndEnabled) return;
            SetOutline(false);
        }

        public virtual void Destroy() { }

        #endregion

        #region Functions
        
        public Transform GetTransform() => gameObject.transform;

        private void SetOutline(bool value) => Outline.enabled = value;

        #endregion
    }
}
