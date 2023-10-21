using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Scripts.Player.PlayerInputs
{
    public class PlayerDebugInputsReader : MonoBehaviour
    {
        public void OnModifyTimeScale(InputValue value) => Time.timeScale += value.Get<float>() / 10;
        public void OnResetTimeScale() => Time.timeScale = 1f;
    }
}
