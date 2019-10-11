using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public enum GameEvent
{
    //UIEvent
    OnSettingRevert,
    OnGraphicLevelChanged,
    OnCommandAlreadyBindKey,
    KeyPressed,
}

class SettingEventData:EventData
{
   
}

class CommandEventData:EventData
{
    public CommandEventData(Command _command)
    {
        Command = _command;
    }
    public Command Command { get; private set; }
}

public abstract class EventData
{

}

class EventManager:Singleton<EventManager>
{
    public delegate void EventListener(object data);

    private Dictionary<GameEvent, List<EventListener> > m_listeners = new Dictionary<GameEvent, List<EventListener> >();

    public override void Init()
    {
        base.Init();
    }

    public void AddListener(GameEvent eventType, EventListener handler)
    {
        if(!m_listeners.ContainsKey(eventType))
        {
            m_listeners[eventType] = new List<EventListener>();
        }
        m_listeners[eventType].Add(handler);
    }

    public void RemoveListener(GameEvent eventType, EventListener handler)
    {
        if ( m_listeners.ContainsKey(eventType) )
        {
            if (m_listeners[eventType] != null)
                m_listeners[eventType].Remove(handler);
        }
    }


    public void RemoveAllListeners(GameEvent eventType, EventListener handler)
    {
        if(m_listeners.ContainsKey(eventType))
        {
            if (m_listeners[eventType] != null)
            {
                m_listeners[eventType].Clear();
            }
        }
    }

    public void DispathEvent(GameEvent eventType,object data = null)
    {
        if (m_listeners.ContainsKey(eventType) && m_listeners[eventType] != null)
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

