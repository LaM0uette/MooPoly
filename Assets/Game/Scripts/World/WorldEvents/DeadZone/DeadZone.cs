using Game.Scripts.Observers;
using Game.Scripts.StaticUtilities;
using UnityEngine;

namespace Game.Scripts.World.WorldEvents.DeadZone
{
    [RequireComponent(typeof(BoxCollider))]
    public class DeadZone : MonoBehaviour
    {
        [SerializeField] private Observer _deadZoneObserver;
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(TagRef.Player)) return;
            
            _deadZoneObserver.Notify();
        }
    }
}
