using Game.Scripts.Observers;
using Game.Scripts.StaticUtilities;
using UnityEngine;

namespace Game.Scripts.MooCoins
{
    [RequireComponent(typeof(Rigidbody))]
    public class MooCoinBehaviour : MonoBehaviour
    {
        #region Statements
        
        public Rigidbody Rigidbody { get; set; } = new();
        public MooCoin MooCoin { get; set; } = new();
        
        [SerializeField] private ObserverEvent _observer;

        private float _speedMovement = 3f;
        private GameObject _playerTarget;

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
        }

        #endregion

        #region Events

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(TagRef.Player)) return;

            _playerTarget = other.gameObject;
            Debug.Log("Player detected");
        }
        
        private void FixedUpdate()
        {
            if (_playerTarget is null) return;
            
            _speedMovement *= 1.3f;
            
            var pos = transform.position;
            var direction = _playerTarget.transform.position - pos;
            direction.y += pos.y;
            
            var distanceThisFrame = _speedMovement * Time.deltaTime;
            
            if (direction.magnitude <= distanceThisFrame)
            {
                Earn();
                return;
            }
            
            transform.Translate(direction.normalized * distanceThisFrame, Space.World);
        }

        #endregion
        
        #region Functions

        private void Earn()
        {
            _observer.Notify(MooCoin);
            Destroy(gameObject);
        }

        #endregion
    }
}
