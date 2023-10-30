using Game.Scripts.Generic.Managers;
using Game.Scripts.Generic.PlayerPref;
using Game.Scripts.Player.PlayerStateMachine;
using Game.Scripts.StaticUtilities;
using UnityEngine;

namespace Game.Scripts.Generic.Handlers
{
    public class MenuSpawnPlayerHandler : MonoBehaviour
    {
        #region Statements
        
        private PlayerStateMachine _playerStateMachine;
        
        private void Awake()
        {
            _playerStateMachine = GameObject.FindGameObjectWithTag(TagRef.Player).GetComponent<PlayerStateMachine>();
        }
        
        private void Start()
        {
            GameManager.Instance.ResetLevelMooCoins();
            
            var position = PlayerPrefsSave.GetPlayerPositionMenu();
            _playerStateMachine.Teleport(position);
        }

        #endregion
    }
}
