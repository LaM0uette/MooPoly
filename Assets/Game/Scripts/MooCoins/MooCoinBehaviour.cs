using Game.Scripts.StaticUtilities;
using UnityEngine;

namespace Game.Scripts.MooCoins
{
    public class MooCoinBehaviour : MonoBehaviour
    {
        #region Statements

        private float _speedMovement = 3f;
        private GameObject _playerTarget;

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
            
            //_speedMovement += Time.deltaTime * 6;
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
            //_observableEvent.Notify(_candyData);
            Destroy(gameObject);
        }

        #endregion
    }
}
