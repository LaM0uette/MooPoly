using Game.Scripts._Data.Observer;
using Game.Scripts._Data.SceneData;
using Game.Scripts.Interactables;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Scripts._Observers.LevelLoaderObserver
{
    public class LevelLoaderObserver : Interactable
    {
        #region Statements

        [Space, Title("Settings")]
        public bool IsUnlocked;
        
        [Space, Title("Observer"), SerializeField] private ObserverEvent _observer;
        [Space, Title("SceneData"), SerializeField] private SceneData _sceneData;
        
        [Space, Title("Materials")]
        [SerializeField] private Material _unlockMaterial;
        [SerializeField] private Material _lockMaterial;
        [SerializeField] private MeshRenderer _meshRenderer;
        
        private void Start()
        {
            _meshRenderer.material = IsUnlocked ? _unlockMaterial : _lockMaterial;
        }

        #endregion

        #region Functions

        public override void Interact()
        {
            if (!IsUnlocked) return;
            
            _observer.Notify(_sceneData);
        }
        
        public override void Enter()
        {
            if (!IsUnlocked) return;
            
            base.Enter();
        }
        
        public override void Exit()
        {
            if (!IsUnlocked) return;
            
            base.Exit();
        }

        #endregion
    }
}
