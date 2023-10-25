using UnityEngine;

namespace Game.Scripts.Generic.Handlers
{
    public class EnvironmentHandler : MonoBehaviour
    {
        #region Statements

        [SerializeField] private MeshRenderer _fogRenderer;
        
        private void Start()
        {
            ActiveRuntimeGameObjects();
        }

        #endregion

        #region Functions

        private void ActiveRuntimeGameObjects()
        {
            _fogRenderer.enabled = true;
        }

        #endregion
    }
}
