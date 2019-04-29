﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public enum GameEvent
{
    testEvent = 0,
}

public abstract class EventData
{

}

class EventManager:Singleton<EventManager>
{
    public delegate void EventListener(EventData data);

    private Dictionary<GameEvent, List<EventListener> > m_listeners = new Dictionary<GameEvent, List<EventListener> >();

    public override void Init()
    {
        base.Init();
    }

    public void AddListener(GameEvent eventType, EventListener handler)
    {
        if(m_listeners[eventType] == null)
        {
            m_listeners[eventType] = new List<EventListener>();
        }
        m_listeners[eventType].Add(handler);
    }

    public void RemoveListener(GameEvent eventType, EventListener handler)
    {
        if (m_listeners[eventType] != null)
        {
            m_listeners[eventType].Remove(handler);
        }
    }


    public void RemoveAllListeners(GameEvent eventType, EventListener handler)
    {
        if (m_listeners[eventType] != null)
        {
            m_listeners[eventType].Clear();
        }
    }

    public void DispathEvent(GameEvent eventType,EventData data)
    {
        if (m_listeners[eventType] != null)
        {
            var list = m_listeners[eventType];
            for(int i = 0;i < list.Count;i++)
            {
                list[i].Invoke(data);
            }
        }
    }

    public override void DeInit()
    {
        base.DeInit();
        m_listeners.Clear();
    }
}

