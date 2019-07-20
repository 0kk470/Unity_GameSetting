using System.Collections.Generic;
using UnityEngine;

public abstract class Command
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

class SettingCommand : Command
{
    public SettingCommand(KeyCode key = KeyCode.Escape) : base(key) { }

    public override void executeOnDown()
    {
        base.executeOnDown();
        if (UIManager.Instance.IsUIOpen("GameSettingUI"))
        {
            UIManager.Instance.CloseUI("GameSettingUI");
        }
        else
        {
            UIManager.Instance.OpenUI("GameSettingUI");
        }
    }
}

class InputManager : Singleton<InputManager>
{
    private List<Command> m_Commands = new List<Command>();

    private Vector2 m_direction = Vector2.zero;

    public Vector2 direction
    {
        get
        {
            return m_direction;
        }
        private set
        {
            m_direction = value;
        }
    }

    private bool isInSetting = false;
    public override void Init()
    {
        base.Init();
        m_Commands.Add(new SettingCommand());
    }

    public override void DeInit()
    {
        m_Commands.Clear();
        base.DeInit();
    }

    public void Update()
    {
        if (isInSetting)
            return;
        UpdateMove();
        UpdateCommands();
    }

    private void UpdateCommands()
    {
        foreach (var command in m_Commands)
        {
            if (Input.GetKeyDown(command.key))
            {
                command.executeOnDown();
            }
            if (Input.GetKeyUp(command.key))
            {
                command.executeOnUp();
            }
            if (Input.GetKey(command.key))
            {
                command.executeOnHold();
            }
        }
    }

    private void UpdateMove()
    {
        //Left
        if (Input.GetKey(SettingManager.Instance.MoveLeft))
        {
            m_direction.x -= 1;
        }
        else if (Input.GetKeyUp(SettingManager.Instance.MoveLeft))
        {
            if (m_direction.x < 0)
                m_direction.x = 0;
        }
        //Right
        if (Input.GetKey(SettingManager.Instance.MoveRight))
        {
            m_direction.x += 1;
        }
        else if (Input.GetKeyUp(SettingManager.Instance.MoveRight))
        {
            if (m_direction.x > 0)
                m_direction.x = 0;
        }
        //Up
        if (Input.GetKey(SettingManager.Instance.MoveUp))
        {
            m_direction.y += 1;
        }
        else if (Input.GetKeyUp(SettingManager.Instance.MoveUp))
        {
            if (m_direction.y > 0)
                m_direction.y = 0;
        }
        //Down
        if (Input.GetKey(SettingManager.Instance.MoveDown))
        {
            m_direction.y -= 1;
        }
        else if (Input.GetKeyUp(SettingManager.Instance.MoveDown))
        {
            if (m_direction.y < 0)
                m_direction.y = 0;
        }
        m_direction.x = Mathf.Clamp(m_direction.x, -1, 1);
        m_direction.y = Mathf.Clamp(m_direction.y, -1, 1);
        m_direction = m_direction.normalized;
    }

    public void BindCommandWithKey(Command command, KeyCode newkey)
    {
        var existedCommand = FindCommandByKey(newkey);
        if (existedCommand != null)
        {
            EventManager.Instance.DispathEvent(GameEvent.OnCommandAlreadyBindKey, new CommandEventData(existedCommand));
            existedCommand.key = KeyCode.None;
        }
        command.key = newkey;
    }

    public Command FindCommandByKey(KeyCode key)
    {
        Command result = null;
        if (m_Commands != null)
        {
            result = m_Commands.Find(command => { return command.key == key; });
        }
        return result;
    }

    public IList<Command> GetAllCommands()
    {
        return m_Commands;
    }
}