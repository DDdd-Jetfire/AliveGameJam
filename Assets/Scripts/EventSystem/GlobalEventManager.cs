using System;
using System.Collections.Generic;
using UnityEngine;

public class GlobalEventManager : MonoBehaviour
{
    private Dictionary<string, Action> _events = new Dictionary<string, Action>();

    private Dictionary<string, Delegate> _paramEvents = new Dictionary<string, Delegate>();

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

    // ========= 新增带参数的事件系统 =========
    // 注册带参数的事件
    public void RegisterEvent<T>(string eventName, Action<T> handler)
    {
        if (_paramEvents.TryGetValue(eventName, out Delegate existingDelegate))
        {
            _paramEvents[eventName] = Delegate.Combine(existingDelegate, handler);
        }
        else
        {
            _paramEvents.Add(eventName, handler);
        }
    }

    // 注销带参数的事件
    public void UnregisterEvent<T>(string eventName, Action<T> handler)
    {
        if (_paramEvents.TryGetValue(eventName, out Delegate existingDelegate))
        {
            Delegate newDelegate = Delegate.Remove(existingDelegate, handler);

            if (newDelegate == null)
            {
                _paramEvents.Remove(eventName);
            }
            else
            {
                _paramEvents[eventName] = newDelegate;
            }
        }
    }

    // 触发带参数的事件
    public void TriggerEvent<T>(string eventName, T eventData)
    {
        if (_paramEvents.TryGetValue(eventName, out Delegate thisDelegate))
        {
            // 安全类型检查
            if (thisDelegate is Action<T> typedEvent)
            {
                typedEvent?.Invoke(eventData);
            }
            else
            {
                Debug.LogError($"Event {eventName} has mismatched type! Expected {typeof(Action<T>).Name}");
            }
        }
    }
    // 清除所有带参数的事件（可选）
    public void ClearAllParamEvents()
    {
        _paramEvents.Clear();
    }

    // 清除特定带参数的事件（可选）
    public void ClearParamEvent(string eventName)
    {
        if (_paramEvents.ContainsKey(eventName))
        {
            _paramEvents.Remove(eventName);
        }
    }
}
