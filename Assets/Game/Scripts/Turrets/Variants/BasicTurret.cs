using Game.Scripts.Enemies.EnemyStateMachine;
using UnityEngine;

namespace Game.Scripts.Turrets.Variants
{
    public class BasicTurret : Turret
    {
        #region Functions

        public override void Shoot(EnemyStateMachine enemy)
        {
            Debug.Log("Shoot");
        }

        #endregion
    }
}
