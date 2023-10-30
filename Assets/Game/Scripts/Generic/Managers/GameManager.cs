using UnityEngine;

namespace Game.Scripts.Generic.Managers
{
    public class GameManager : MonoBehaviour
    {
        #region Statements

        public static GameManager Instance;
        
        public int CurrentLevelMooCoins { get; private set; }

        private void Awake()
        {
            if (Instance is null) 
                Instance = this;
            else 
                Destroy(gameObject);
        }

        #endregion

        #region Functions
        
        public void IncreaseLevelMooCoins(int value) => CurrentLevelMooCoins += value;
        public void DecreaseLevelMooCoins(int value) => CurrentLevelMooCoins -= value;
        public void ResetLevelMooCoins() => CurrentLevelMooCoins = 0;

        #endregion
    }
}
