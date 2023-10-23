using Game.Scripts.Interactable;
using Game.Scripts.Observers;
using UnityEngine;

namespace Game.Scripts.Ui.World.Button.Level
{
    public class ButtonLevel : MonoBehaviour, IInteract
    {
        #region Statements

        [SerializeField] private Observer _observer;
        
        [SerializeField] private Material _unlockMaterial;
        [SerializeField] private Material _lockMaterial;
        [SerializeField] private MeshRenderer _meshRenderer;
        
        [SerializeField] private bool _isUnlocked;

        private void Start()
        {
            _meshRenderer.material = _isUnlocked ? _unlockMaterial : _lockMaterial;
        }

        #endregion

        #region Functions

        public void Interact()
        {
            if (!_isUnlocked) return;
            
            _observer.Notify();
        }
        
        public Transform GetTransform()
        {
            return gameObject.transform;
        }

        #endregion
    }
}
