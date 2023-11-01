using Game.Scripts._Data.BulletData;

namespace Game.Scripts.Bullets.BulletFactory
{
    public static class BulletFactory
    {
        public static Bullet Create(BulletData bulletData)
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
