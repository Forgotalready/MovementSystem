using System;
using System.Collections.Generic;

public class ControllersEventBus
{
    Dictionary<Type, List<object>> _subscribers = new();
    
    public void Subscribe<T>(Action<T> callback)
    {
        Type type = typeof(T);
        if (_subscribers.ContainsKey(type))
        {
            _subscribers[type].Add(callback);
        }
        else
        {
            _subscribers[type] = new List<object> { callback };
        }
    }

    public void Unsubscribe<T>(Action<T> callback)
    {
        Type type = typeof(T);
        if (_subscribers.ContainsKey(type))
        {
            _subscribers[type].Remove(callback);
        }
    }

    public void Publish<T>(T signal)
    {
        Type type = typeof(T);
        if (_subscribers.TryGetValue(type, out List<object> subscriber))
        {
            foreach (object obj in subscriber)
            {
                var callback = obj as Action<T>;
                callback?.Invoke(signal);
            }
        }
    }
}