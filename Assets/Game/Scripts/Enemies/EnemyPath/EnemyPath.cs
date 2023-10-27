using Game.Scripts.Observers;
using UnityEngine;
using UnityEngine.Splines;

namespace Game.Scripts.Enemies.EnemyPath
{
    public class EnemyPath : MonoBehaviour
    {
        #region Statements

        [SerializeField] private ObserverEvent _observer;
        
        private SplineContainer _enemyPath;

        private void Awake()
        {
            _enemyPath = GetComponent<SplineContainer>();
        }

        private void Start()
        {
            _observer.Notify(_enemyPath);
        }

        #endregion
    }
}
