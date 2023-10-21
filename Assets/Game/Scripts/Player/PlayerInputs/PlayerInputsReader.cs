using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Scripts.Player.PlayerInputs
{
    public class PlayerInputsReader : MonoBehaviour
    {
        #region Statements

        public Vector2 MoveValue { get; private set; }

        #endregion

        #region Events

        public void OnMove(InputValue value) => MoveValue = value.Get<Vector2>();

        #endregion
    }
}
