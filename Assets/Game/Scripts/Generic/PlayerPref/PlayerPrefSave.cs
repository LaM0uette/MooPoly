using UnityEngine;

namespace Game.Scripts.Generic.PlayerPref
{
    public static class PlayerPrefsSave
    {
        #region Statements

        private const string PlayerPositionMenuHash = "PlayerPositionMenu";

        #endregion

        #region Functions

        public static void SetPlayerPositionMenu(Vector3 position)
        {
            var positionString = position.x + "|" + position.y + "|" + position.z;
            PlayerPrefs.SetString(PlayerPositionMenuHash, positionString);
        }
        
        public static Vector3 GetPlayerPositionMenu()
        {
            if (!PlayerPrefs.HasKey(PlayerPositionMenuHash)) return Vector3.zero;
            
            var positionString = PlayerPrefs.GetString(PlayerPositionMenuHash);
            var positionSplit = positionString.Split('|');
            var position = new Vector3(float.Parse(positionSplit[0]), float.Parse(positionSplit[1]), float.Parse(positionSplit[2]));
            return position;
        }
        
        public static void DeletePlayerPositionMenu()
        {
            if (!PlayerPrefs.HasKey(PlayerPositionMenuHash)) return;
            
            PlayerPrefs.DeleteKey(PlayerPositionMenuHash);
        }

        #endregion
    }
}
