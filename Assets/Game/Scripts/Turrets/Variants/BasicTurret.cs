using Game.Scripts.Enemies.EnemyFactory;
using UnityEngine;

namespace Game.Scripts.Turrets.Variants
{
    public class BasicTurret : Turret
    {
        #region Functions

        public override void Shoot(Enemy enemy)
        {
            Debug.Log("Shoot");
        }

        #endregion
    }
}
