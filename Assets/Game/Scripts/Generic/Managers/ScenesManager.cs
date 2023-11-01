using Game.Scripts._Data.SceneData;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Scripts.Generic.Managers
{
    public class ScenesManager : MonoBehaviour
    {
        #region Statements

        private static ScenesManager Instance;

        private void Awake()
        {
            if (Instance is null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        #endregion

        #region Functions

        public static void LoadScene(SceneData sceneData)
        {
            SceneManager.LoadScene(sceneData.Name);
        }

        #endregion
    }
}
