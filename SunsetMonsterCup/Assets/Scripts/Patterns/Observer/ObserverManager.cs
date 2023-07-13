using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObserverManager : MonoBehaviour
{
    private Dictionary<string, List<IObserver>> observers = new();

    public void Subscribe(string eventName, IObserver observer)
    {
        if (!observers.ContainsKey(eventName))
            observers[eventName] = new List<IObserver>();

        observers[eventName].Add(observer);
    }

    public void Unsubscribe(string eventName, IObserver observer)
    {
        if (observers.ContainsKey(eventName))
            observers[eventName].Remove(observer);
    }

    public void Notify(string eventName)
    {
        if (observers.ContainsKey(eventName))
        {
            foreach (var observer in observers[eventName])
            {
                observer.OnNotify(eventName);
            }
        }
    }
}

