using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Observers
{
    [CreateAssetMenu(menuName = "Observer/Observer")]
    public class Observer : ScriptableObject
    {
        private readonly List<IObserver> _observers = new();

        public void Register(IObserver observer)
        {
            if (!_observers.Contains(observer))
                _observers.Add(observer);
        }

        public void Unregister(IObserver observer)
        {
            if (_observers.Contains(observer))
                _observers.Remove(observer);
        }

        public void Notify()
        {
            for (var i = _observers.Count - 1; i >= 0; i--)
                _observers[i].OnNotify();
        }
    }
}
