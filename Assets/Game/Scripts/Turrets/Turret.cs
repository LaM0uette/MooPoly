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
        
        // Stats
        public float Damage { get; set; }
        public float Range { get; set; }
        public float FireRate { get; set; }
        public float RotationSpeed { get; set; }
        public float RepeatRate { get; set; }

        #endregion

        #region Functions

        public void Init(TurretData turretData)
        {
            Type = turretData.Type;
            Damage = turretData.Damage;
            Range = turretData.Range;
            FireRate = turretData.FireRate;
            RotationSpeed = turretData.RotationSpeed;
            RepeatRate = turretData.RepeatRate;
        }

        public abstract void Shoot(Enemy enemy);

        #endregion
    }
}
