using Game.Scripts.Bullets;
using Game.Scripts.Enemies.EnemyStateMachine;

namespace Game.Scripts.Turrets.Variants
{
    public class BasicTurret : Turret
    {
        #region Functions

        public override void Shoot(EnemyStateMachine enemy)
        {
            var bulletGo = Instantiate(Bullet.Prefab, FirePoint.position, FirePoint.rotation, BulletsParent.transform);
            var bulletBehaviour = bulletGo.GetComponent<BulletBehaviour>();
            bulletBehaviour.Init(enemy, Damage);
        }

        #endregion
    }
}
