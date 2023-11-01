using Game.Scripts.Enemies.EnemyStateMachine;

namespace Game.Scripts.Turrets.TurretFactory.Variants
{
    public class BasicTurret : Turret
    {
        #region Functions

        public override void Shoot(EnemyStateMachine enemy)
        {
            InstanciateBullet(enemy);
        }

        #endregion
    }
}
