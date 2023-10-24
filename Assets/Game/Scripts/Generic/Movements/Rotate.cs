using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Scripts.Generic.Movements
{
    public class Rotate : MonoBehaviour
    {
        #region Statements

        private enum Axis
        {
            X,
            Y,
            Z
        }

        [SerializeField, EnumToggleButtons] private Axis _axis;
        [SerializeField] private float _speed = 1f;
        
        private Vector3 _axisVector;

        private void Awake()
        {
            _axisVector = _axis switch
            {
                Axis.X => Vector3.right,
                Axis.Y => Vector3.up,
                Axis.Z => Vector3.forward,
                _ => Vector3.zero
            };
        }

        #endregion

        #region Events

        private void Update()
        {
            transform.Rotate(_axisVector, _speed * Time.deltaTime);
        }

        #endregion
    }
}
