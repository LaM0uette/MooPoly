using Game.Scripts._Data.Observer;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Scripts.Player.PlayerInputs
{
    public class PlayerDebugInputsReader : MonoBehaviour
    {
        [SerializeField] private ObserverEvent _observer;
        
        public void OnModifyTimeScale(InputValue value) => Time.timeScale += value.Get<float>() / 10;
        public void OnResetTimeScale() => Time.timeScale = 1f;
        
        public void OnGainCoins() => _observer.Notify(1000);
    }
}
