using UnityEngine;

namespace Game.Scripts.Generic.Movements
{
    public class LookAtCamera : MonoBehaviour
    {
        #region Statements

        private Camera _mainCamera;

        private void Start()
        {
            _mainCamera = Camera.main;
        }

        #endregion

        #region Events

        private void Update()
        {
            var cameraTransform = _mainCamera.transform;
            transform.LookAt(cameraTransform);
            transform.rotation = Quaternion.LookRotation(cameraTransform.forward);
        }

        #endregion
    }
}
