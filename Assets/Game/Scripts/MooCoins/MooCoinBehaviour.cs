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
        private float _gravityAcceleration = -16f;
        private float _verticalVelocity;
        private bool _isGrounded;


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
        
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Default")) 
            {
                _isGrounded = true;
                Rigidbody.velocity = Vector3.zero;
            }
        }
        
        private void FixedUpdate()
        {
            if (!_isGrounded && _playerTarget is null)
            {
                _verticalVelocity += _gravityAcceleration * Time.deltaTime;
                transform.position += new Vector3(0, _verticalVelocity * Time.deltaTime, 0);
                return;
            }
    
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
