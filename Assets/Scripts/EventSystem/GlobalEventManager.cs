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
    // ע���¼�
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

    // ע���¼�
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

    // �����¼�
    public void TriggerEvent(string eventName)
    {
        if (_events.TryGetValue(eventName, out Action thisEvent))
        {
            thisEvent?.Invoke();
        }
    }

    // ��������¼�
    public void ClearAllEvents()
    {
        _events.Clear();
    }

    // ���ָ���¼�
    public void ClearEvent(string eventName)
    {
        if (_events.ContainsKey(eventName))
        {
            _events.Remove(eventName);
        }
    }
}
