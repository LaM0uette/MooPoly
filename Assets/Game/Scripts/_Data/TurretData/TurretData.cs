using Sirenix.OdinInspector;
using UnityEngine;

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
        public float BuildTime;
        
        [Space, Title("Stats")]
        public float Damage;
        public float Range;
        public float FireRate;
        public float RotationSpeed;
        public float RepeatRate;
    }
}
