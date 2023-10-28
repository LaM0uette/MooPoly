using Game.Scripts.ScriptableObjects.EnemyData;

namespace Game.Scripts.Enemies.EnemyBuild
{
    public class Enemy
    {
        #region Statements

        // Settings
        public EnemyType Type { get; set; } = EnemyType.Land;
        public float Health { get; set; } = 100;
        public float MoveSpeed { get; set; } = 1;

        // Spawn
        public float HeightOffset { get; set; } = 0;
        public float SpawnRate { get; set; } = 1;

        // Candy
        public int CandyDropMax { get; set; } = 5;
        public float SuperCandyChance { get; set; } = 0.01f;

        #endregion
    }
}
