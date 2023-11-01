using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Farm
{
    public class FarmHandler : MonoBehaviour
    {
        #region Statements

        [SerializeField] private GameObject _cowParent;
        private List<GameObject> _cowsList;

        private void Awake () 
        {
            var cows = new GameObject[_cowParent.transform.childCount];
            
            for (var i = 0; i < cows.Length; i++)
                cows[i] = _cowParent.transform.GetChild(i).gameObject;
            
            _cowsList = new List<GameObject>(cows);
        }
        
        #endregion

        #region Functions

        public void KillCow()
        {
            var cow = _cowsList[Random.Range(0, _cowsList.Count)];
            
            _cowsList.Remove(cow);
            Destroy(cow, 0);
        }

        #endregion
    }
}
