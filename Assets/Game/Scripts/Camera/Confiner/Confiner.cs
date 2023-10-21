using UnityEngine;

namespace Game.Scripts.Camera.Confiner
{
    [RequireComponent(typeof(BoxCollider))]
    public class Confiner : MonoBehaviour
    {
        #region Statements

        public BoxCollider BoxCollider { get; private set; }
        public int MinBoxWidth { get; private set; }
        public int MaxBoxWidth { get; private set; }
        
        [SerializeField] private int _maxMagnification = 10;

        private void Awake()
        {
            BoxCollider = GetComponent<BoxCollider>();
            
            MinBoxWidth = (int) BoxCollider.size.x;
            MaxBoxWidth = MinBoxWidth + _maxMagnification;
        }

        #endregion

        #region Debug

        private void OnDrawGizmos()
        {
            var boxCollider = GetComponent<BoxCollider>();
            if (boxCollider is null) return;
            
            Gizmos.color = Color.green; 
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawWireCube(boxCollider.center, boxCollider.size);
            
            Gizmos.color = Color.red; 
            var newPosition = boxCollider.center;
            newPosition.z += (float)_maxMagnification / 2;
            var newSize = boxCollider.size;
            newSize.x += _maxMagnification;
            Gizmos.DrawWireCube(newPosition, newSize);
        }

        #endregion
    }
}
