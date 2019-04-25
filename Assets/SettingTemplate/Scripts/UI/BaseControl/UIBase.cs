using UnityEngine;

public class UIBase:MonoBehaviour
{
    protected void Awake()
    {
        Init();
    }

    protected void OnDestroy()
    {
        DeInit();
    }

    protected virtual void Init()
    {

    }

    protected virtual void LoadConfig()
    {

    }

    public virtual void Refresh()
    {

    }

    protected virtual void OnShow()
    {

    }

    public virtual void Show(bool bRefresh = true)
    {
        if (bRefresh)
            Refresh();
        gameObject.SetActiveEx(true);
        OnShow();
    }

    protected virtual void OnHide()
    {

    }

    public virtual void Hide()
    {
        gameObject.SetActiveEx(false);
        OnHide();
    }

    protected virtual void DeInit()
    {

    }
}
