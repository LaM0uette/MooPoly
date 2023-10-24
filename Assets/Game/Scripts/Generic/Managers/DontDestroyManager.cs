using UnityEngine;

namespace Game.Scripts.Generic.Managers
{
    public class DontDestroyManager : MonoBehaviour
    {
        private static DontDestroyManager _instance;

        private void Awake()
        {
            if (_instance is null)
                _instance = this;
            else
                Destroy(gameObject);
            
            DontDestroyOnLoad(gameObject);
        }
    }
}
