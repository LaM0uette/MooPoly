using Game.Scripts._Data.BulletData;

namespace Game.Scripts.Bullets
{
    public static class BulletFactory
    {
        public static Bullet CreateBullet(BulletData bulletData)
        {
            return new Bullet
            {
                Prefab = bulletData.Prefab,
                Damage = bulletData.Damage,
                Speed = bulletData.Speed
            };
        }
    }
}
