using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Scripts._Data.TurretData
{
    [CreateAssetMenu(menuName = "MooPloy_Data/TurretData")]
    public class TurretData : ScriptableObject
    {
        [Space, Title("Settings")]
        public string Name;
        [EnumToggleButtons] public TurretType Type;
        
        [Space, Title("Build")]
        public int Cost;
        [Range(0, 1f)] public float BuildTime;
        
        [Space, Title("Stats")]
        public float Damage;
        public float Range;
        public float FireRate;
        public float RotationSpeed;
        
        [Space, Title("Bullet")]
        public BulletData.BulletData BulletData;
    }
}
