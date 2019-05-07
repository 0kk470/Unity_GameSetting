using UnityEngine;

abstract class Commmand
{
    public virtual void execute() { }
}

class InputManager:Singleton<InputManager>
{
    private Commmand moveLeft;

    public override void Init()
    {
        base.Init();
    }

    public override void DeInit()
    {
        base.DeInit();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
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
}
