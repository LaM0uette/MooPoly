using UnityEngine;

namespace Game.Scripts.Bullets.BulletFactory
{
    public class Bullet
    {
        public GameObject Prefab { get; set; }
        public float Damage { get; set; }
        public float Speed { get; set; } = 100f;
    }
}
