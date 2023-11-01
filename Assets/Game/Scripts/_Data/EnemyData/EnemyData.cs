using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Scripts._Data.EnemyData
{
    [Serializable]
    public struct MooCoinProbability
    {
        public MooCoinData.MooCoinData MooCoinData;
        [Range(0, 1f)] public float Probability;
    }
    
    [CreateAssetMenu(menuName = "MooPloy_Data/EnemyData")]
    public class EnemyData : ScriptableObject
    {
        [Space, Title("Settings")]
        [EnumToggleButtons] public EnemyType Type;
        public GameObject Prefab;
        
        public float BaseLife;
        public float MoveSpeed;
        
        [Space, Title("Spawn")]
        public float HeightOffset;
        public float SpawnRate;
        
        [Space, Title("Coin")]
        [SerializeField] private List<MooCoinProbability> _mooCoins = new();
        public MooCoinProbability[] MooCoins => _mooCoins.ToArray();
    }
}
