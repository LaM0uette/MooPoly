using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Scripts.ScriptableObjects.EnemyData
{
    [CreateAssetMenu(menuName = "MooPloy_Data/EnemyData")]
    public class EnemyData : ScriptableObject
    {
        [Space, Title("Settings")]
        [EnumToggleButtons] public EnemyType Type;
        public float BaseLife;
        public float MoveSpeed;
        
        [Space, Title("Spawn")]
        public float HeightOffset;
        public float SpawnRate;
        
        [Space, Title("Candy")]
        public int CandyDropMax;
        [Range(0, 1f)] public float SuperCandyChance;
    }
}