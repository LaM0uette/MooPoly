using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Scripts.Player.PlayerInputs
{
    public class PlayerInputsReader : MonoBehaviour
    {
        #region Statements

        public Vector2 MoveValue { get; private set; }
        public float ZoomValue { get; private set; }

        public Action JumpEvent { get; set; }
        public Action LevelMapEvent { get; set; }
        
        #endregion

        #region Events

        public void OnMove(InputValue value) => MoveValue = value.Get<Vector2>();
        public void OnZoom(InputValue value) => ZoomValue = value.Get<float>();

        public void OnJump() => JumpEvent?.Invoke();
        public void OnLevelMap() => LevelMapEvent?.Invoke();
        
        #endregion
    }
}
