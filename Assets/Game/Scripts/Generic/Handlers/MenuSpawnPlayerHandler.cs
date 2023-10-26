using Game.Scripts.Generic.PlayerPref;
using Game.Scripts.Player.PlayerStateMachine;
using UnityEngine;

namespace Game.Scripts.Generic.Handlers
{
    public class MenuSpawnPlayerHandler : MonoBehaviour
    {
        #region Statements
        
        [SerializeField] private PlayerStateMachine _playerStateMachine;
        
        private void Start()
        {
            var position = PlayerPrefsSave.GetPlayerPositionMenu();
            _playerStateMachine.Teleport(position);
        }

        #endregion
    }
}
