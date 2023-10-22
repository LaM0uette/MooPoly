using Game.Scripts.Player.PlayerStateMachine;
using Game.Scripts.StaticUtilities;
using UnityEngine;

namespace Game.Scripts.World.WorldEvents.ZoneEvents
{
    [RequireComponent(typeof(BoxCollider))]
    public class DeadZone : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(TagRef.Player)) return;

            var playerStateMachine = other.gameObject.GetComponent<PlayerStateMachine>();
            playerStateMachine.Dead();
        }
    }
}
