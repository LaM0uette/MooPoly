using Game.Scripts._Data.TurretData;
using Game.Scripts.Bullets;
using Game.Scripts.Enemies.EnemyStateMachine;
using Game.Scripts.StaticUtilities;
using UnityEngine;

namespace Game.Scripts.Turrets
{
    public abstract class Turret : MonoBehaviour
    {
        #region Statements
        
        public static GameObject BulletsParent { get; private set; }

        // Settings
        public TurretType Type { get; private set; }
        
        // Build
        public int Cost { get; private set; }
        public float BuildTime { get; private set; }
        public Transform FirePoint { get; private set; }
        
        // Stats
        public float Damage { get; private set; }
        public float Range { get; private set; }
        public float FireRate { get; private set; }
        public float RotationSpeed { get; private set; }
        
        // Bullet
        public Bullet Bullet;

        private void Awake()
        {
            BulletsParent = GameObject.FindGameObjectWithTag(TagRef.BulletsParent);
        }

        #endregion

        #region Functions

        public void Init(TurretData turretData, Transform firePoint)
        {
            Type = turretData.Type;
            Cost = turretData.Cost;
            BuildTime = turretData.BuildTime;
            FirePoint = firePoint;
            Damage = turretData.Damage;
            Range = turretData.Range;
            FireRate = turretData.FireRate;
            RotationSpeed = turretData.RotationSpeed;
            Bullet = BulletFactory.Create(turretData.BulletData);
        }

        protected void InstanciateBullet(EnemyStateMachine enemy)
        {
            var bulletGo = Instantiate(Bullet.Prefab, FirePoint.position, FirePoint.rotation, BulletsParent.transform);
            var bulletBehaviour = bulletGo.GetComponent<BulletBehaviour>();
            bulletBehaviour.Init(enemy, Damage);
        }

        public abstract void Shoot(EnemyStateMachine enemy);

        #endregion
    }
}
