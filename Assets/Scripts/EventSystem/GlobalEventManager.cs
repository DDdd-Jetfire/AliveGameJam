using System;
using System.Collections.Generic;
using UnityEngine;

public class GlobalEventManager : MonoBehaviour
{
    private Dictionary<string, Action> _events = new Dictionary<string, Action>();


    public static GlobalEventManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // 注册事件
    public void RegisterEvent(string eventName, Action handler)
    {
        if (_events.ContainsKey(eventName))
        {
            _events[eventName] += handler;
        }
        else
        {
            _events.Add(eventName, handler);
        }
    }

    // 注销事件
    public void UnregisterEvent(string eventName, Action handler)
    {
        if (_events.TryGetValue(eventName, out Action existingEvent))
        {
            existingEvent -= handler;

            if (existingEvent == null)
            {
                _events.Remove(eventName);
            }
            else
            {
                _events[eventName] = existingEvent;
            }
        }
    }

    // 触发事件
    public void TriggerEvent(string eventName)
    {
        if (_events.TryGetValue(eventName, out Action thisEvent))
        {
            thisEvent?.Invoke();
        }
    }

    // 清除所有事件
    public void ClearAllEvents()
    {
        _events.Clear();
    }

    // 清除指定事件
    public void ClearEvent(string eventName)
    {
        if (_events.ContainsKey(eventName))
        {
            _events.Remove(eventName);
        }
    }
}
