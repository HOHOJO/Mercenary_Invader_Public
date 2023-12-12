using System;
using System.Collections.Generic;
using UnityEngine;

public enum HealthState
{
    Normal,
    Low,
    Critical,
}

public interface IHealthObserver
{
    void OnHealthStateChanged(HealthState newState);
}

public class HealthStateObserver
{
    private List<IHealthObserver> observers = new List<IHealthObserver>();

    public void Subscribe(IHealthObserver observer)
    {
        if (!observers.Contains(observer))
        {
            observers.Add(observer);
        }
    }

    public void Unsubscribe(IHealthObserver observer)
    {
        observers.Remove(observer);
    }

    public void Notify(HealthState newState)
    {
        foreach (var observer in observers)
        {
            observer.OnHealthStateChanged(newState);
        }
    }
}
