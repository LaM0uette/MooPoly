using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Modules.Generic.Movements
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

        #endregion

        #region Events

        private void Update()
        {
            var axis = _axis switch
            {
                Axis.X => Vector3.right,
                Axis.Y => Vector3.up,
                Axis.Z => Vector3.forward,
                _ => Vector3.zero
            };

            transform.Rotate(axis, _speed * Time.deltaTime);
        }

        #endregion
    }
}
