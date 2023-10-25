using Game.Scripts.Generic.PlayerPref;
using UnityEngine;

namespace Game.Scripts.Generic.Managers
{
    public class MenuManager : MonoBehaviour
    {
        #region Statements
        
        private void Start()
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            player.transform.position = PlayerPrefsSave.GetPlayerPositionMenu();
        }

        #endregion
    }
}
