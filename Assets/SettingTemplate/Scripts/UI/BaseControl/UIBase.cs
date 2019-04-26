﻿using UnityEngine;

public class UIBase:MonoBehaviour
{
    private bool   bOpen = false;
    public  string uiName { get; set; }
    protected UIBase[] childsUI;
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
        bOpen = true;
        OnShow();
    }

    protected virtual void OnHide()
    {

    }

    public virtual void Hide()
    {
        gameObject.SetActiveEx(false);
        bOpen = false;
        OnHide();
    }

    public virtual void Close()
    {
        Hide();
        UIManager.Instance.RemoveUI(uiName);
        Destroy(gameObject);
    }

    public bool IsOpen()
    {
        return bOpen;
    }

    protected virtual void DeInit()
    {
        if(childsUI != null)
        {
            for (int i = 0; i < childsUI.Length; i++)
                childsUI[i].Close();
        }
    }
}
