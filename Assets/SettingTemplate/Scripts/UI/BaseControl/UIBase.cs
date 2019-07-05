using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBase:MonoBehaviour
{
    private bool   bOpen = false;
    public  string uiName { get; set; }
    protected UIBase[] childsUI;
    protected Dictionary<uint, Coroutine> m_Coroutines = new Dictionary<uint, Coroutine>();
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
        gameObject.SetActiveEx(true);
        if (bRefresh)
            Refresh();
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
        StopAllCoroutines();
        if (m_Coroutines != null)
        {
            m_Coroutines.Clear();
        }
    }

    public void DoSomethingAsync(uint uiCoroutineID,Action callback,float delayTime = 0, int iLoop = 1, Action onComplete = null)
    {
        Coroutine co = null;
        if(m_Coroutines.TryGetValue(uiCoroutineID, out co))
        {
            StopCoroutine(co);
            m_Coroutines.Remove(uiCoroutineID);
        }
        co = StartCoroutine(AsyncOperation(callback, delayTime, iLoop, onComplete));
        m_Coroutines.Add(uiCoroutineID,co);
    }

    private IEnumerator AsyncOperation(Action callback,float delayTime, int iLoop, Action onComplete)
    {
        var waitTime = delayTime > 0 ? new WaitForSeconds(delayTime) : null;
        while(iLoop-- > 0)
        {
            if (callback != null)
                callback();
            yield return waitTime;
        }
        if (onComplete != null)
            onComplete();
    }
}
