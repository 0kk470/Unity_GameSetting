using System.Collections.Generic;
using UnityEngine;

abstract class Command
{
    private KeyCode m_KeyCode = KeyCode.None;

    public KeyCode key
    {
        get
        {
            return m_KeyCode;
        }
        set
        {
            m_KeyCode = value;
        }
    }

    public Command(KeyCode key)
    {
        m_KeyCode = key;
    }

    public virtual void executeOnDown() { }
    public virtual void executeOnUp() { }
    public virtual void executeOnHold() { }
}

class InputManager : Singleton<InputManager>
{
    private List<Command> m_Commands = new List<Command>();
    private bool isInSetting = false;
    public override void Init()
    {
        base.Init();
    }

    public override void DeInit()
    {
        m_Commands.Clear();
        base.DeInit();
    }

    public void Update()
    {
        if (!isInSetting)
        {
            foreach (var command in m_Commands)
            {
                if (Input.GetKey(command.key))
                {
                    command.executeOnHold();
                }
                else if (Input.GetKeyUp(command.key))
                {
                    command.executeOnUp();
                }
                else if (Input.GetKeyDown(command.key))
                {
                    command.executeOnDown();
                }
            }
        }
        else
        {

        }
    }

    public void BindCommandWithKey(Command command,KeyCode newkey)
    {
        var existedCommand = FindCommandByKey(newkey);
        if(existedCommand != null)
        {
            EventManager.Instance.DispathEvent(GameEvent.OnCommandAlreadyBindKey, new CommandEventData(existedCommand));
            existedCommand.key = KeyCode.None;
        }
        command.key = newkey;
    }

    public Command FindCommandByKey(KeyCode key)
    {
        Command result = null;
        if(m_Commands != null)
        {
            result = m_Commands.Find( command => { return command.key == key; });
        }
        return result;
    }
}
