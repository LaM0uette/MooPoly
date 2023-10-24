using Game.Scripts.Interactable;
using Game.Scripts.Observers;
using UnityEngine;

namespace Game.Scripts.Ui.World.Button.Level
{
    public class ButtonLevel : MonoBehaviour, IInteract
    {
        #region Statements

        public bool IsUnlocked;
        
        [SerializeField] private ObserverEvent _observer;
        
        [SerializeField] private Material _unlockMaterial;
        [SerializeField] private Material _lockMaterial;
        [SerializeField] private MeshRenderer _meshRenderer;
        
        private void Start()
        {
            _meshRenderer.material = IsUnlocked ? _unlockMaterial : _lockMaterial;
        }

        #endregion

        #region Functions

        public void Interact()
        {
            if (!IsUnlocked) return;
            
            _observer.Notify(this);
        }
        
        public Transform GetTransform() => gameObject.transform;

        #endregion
    }
}
