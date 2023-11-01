using Game.Scripts._Data.Observer;
using UnityEngine;
using UnityEngine.Splines;

namespace Game.Scripts._Observers.EnemyPathObserver
{
    public class EnemyPathObserver : MonoBehaviour
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
