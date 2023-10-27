using Game.Scripts.ScriptableObjects.EnemyData;
using UnityEngine;

namespace Game.Scripts.Enemies
{
    public class Enemy : MonoBehaviour
    {
        #region Statements
        
        public EnemyData EnemyData;

        public bool IsDead { get; private set; }
        
        protected float Health { get; private set; }

        #endregion
    }
}
