using Game.Scripts._Data.TurretData;
using Game.Scripts.Enemies.EnemyFactory;
using UnityEngine;

namespace Game.Scripts.Turrets
{
    public abstract class Turret : MonoBehaviour
    {
        #region Statements

        // Settings
        public TurretType Type { get; set; }
        
        // Build
        public float BuildTime { get; set; }
        
        // Stats
        public float Damage { get; set; }
        public float Range { get; set; }
        public float FireRate { get; set; }
        public float RotationSpeed { get; set; }

        #endregion

        #region Functions

        public void Init(TurretData turretData)
        {
            Type = turretData.Type;
            BuildTime = turretData.BuildTime;
            Damage = turretData.Damage;
            Range = turretData.Range;
            FireRate = turretData.FireRate;
            RotationSpeed = turretData.RotationSpeed;
        }

        public abstract void Shoot(Enemy enemy);

        #endregion
    }
}
