using UnityEngine;

namespace Game.Scripts.Generic.Managers
{
    public class GameManager : MonoBehaviour
    {
        #region Statements

        public static GameManager Instance;

        private void Awake()
        {
            if (Instance is null) 
                Instance = this;
            else 
                Destroy(gameObject);
        }

        #endregion
    }
}
