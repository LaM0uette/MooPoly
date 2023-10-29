using UnityEngine;

namespace Game.Scripts._Data.MooCoinData
{
    [CreateAssetMenu(menuName = "MooPloy_Data/MooCoinData")]
    public class MooCoinData : ScriptableObject
    {
        public GameObject Prefab;
        public int CandyEarned;
    }
}
