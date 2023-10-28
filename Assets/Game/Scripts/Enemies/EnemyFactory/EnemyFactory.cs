using Game.Scripts._Data.EnemyData;

namespace Game.Scripts.Enemies.EnemyFactory
{
    public static class EnemyFactory
    {
        public static Enemy CreateEnemy(EnemyData enemyData, float newHealth)
        {
            return new Enemy
            {
                Type = enemyData.Type,
                Health = newHealth,
                MoveSpeed = enemyData.MoveSpeed,
                HeightOffset = enemyData.HeightOffset,
                SpawnRate = enemyData.SpawnRate,
                CandyDropMax = enemyData.CandyDropMax,
                SuperCandyChance = enemyData.SuperCandyChance
            };
        }
    }
}
