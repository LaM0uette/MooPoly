using UnityEngine;

namespace Game.Scripts._Data.Observer
{
    public abstract class Observer : MonoBehaviour
    {
        public virtual void OnNotify() { }
        public virtual void OnNotify<T>(T data) { }
    }
}
