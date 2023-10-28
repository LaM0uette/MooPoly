using UnityEngine;

namespace Game.Scripts.BaseStateMachine
{
    public class StateMachine : MonoBehaviour
    {
        private State _currentState;

        public void SwitchState(State newState)
        {
            _currentState?.Exit();
            _currentState = newState;
            _currentState?.Enter();
        }

        private void Update()
        {
            _currentState.Tick(Time.deltaTime);
            _currentState.CheckState();
        }
        
        private void LateUpdate()
        {
            _currentState.TickLate(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            _currentState.TickFixed(Time.deltaTime);
        }
    }
}
