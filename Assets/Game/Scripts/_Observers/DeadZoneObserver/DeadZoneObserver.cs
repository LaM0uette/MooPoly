using Game.Scripts._Data.Observer;
using Game.Scripts.StaticUtilities;
using UnityEngine;

namespace Game.Scripts._Observers.DeadZoneObserver
{
    [RequireComponent(typeof(BoxCollider))]
    public class DeadZoneObserver : MonoBehaviour
    {
        [SerializeField] private ObserverEvent _observer;
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(TagRef.Player)) return;
            
            _observer.Notify();
        }
    }
}
