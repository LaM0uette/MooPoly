using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Observers
{
    [CreateAssetMenu(menuName = "Observer/ObserverEvent")]
    public class ObserverEvent : ScriptableObject
    {
        private readonly List<Observer> _observers = new();

        public void Register(Observer observer)
        {
            if (!_observers.Contains(observer))
                _observers.Add(observer);
        }

        public void Unregister(Observer observer)
        {
            if (_observers.Contains(observer))
                _observers.Remove(observer);
        }

        public void Notify()
        {
            foreach (var observer in _observers)
                observer.OnNotify();
        }

        public void Notify<T>(T data)
        {
            foreach (var observer in _observers)
                observer.OnNotify(data);
        }
    }
}
