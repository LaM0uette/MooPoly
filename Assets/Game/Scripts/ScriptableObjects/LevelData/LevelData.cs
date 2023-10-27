using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.ScriptableObjects.LevelData
{
    [Serializable]
    public struct Wave
    {
        public GameObject EnemyPrefab;
        public int EnemyCount;
        [Range(0, 5)] public int PathIndex;
    }
    
    [CreateAssetMenu(menuName = "MooPloy_Data/LevelData")]
    public class LevelData : ScriptableObject
    {
        [SerializeField] private List<Wave> waves = new();
        public Wave[] Waves => waves.ToArray();
    }
}