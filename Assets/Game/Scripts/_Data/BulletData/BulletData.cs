using UnityEngine;

namespace Game.Scripts._Data.BulletData
{
    [CreateAssetMenu(menuName = "MooPloy_Data/BulletData")]
    public class BulletData : ScriptableObject
    {
        public GameObject Prefab;
        public float Damage;
        public float Speed;
    }
}
