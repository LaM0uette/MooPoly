using Game.Scripts._Data.EnemyData;
using UnityEngine;

namespace Game.Scripts.Enemies.EnemyFactory
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

        // Dead
        public Vector3 DeadPosition { get; set; }
        public bool IsDead { get; set; }
        
        // MooCoins
        public MooCoinProbability[] MooCoins { get; set; }

        #endregion
    }
}
