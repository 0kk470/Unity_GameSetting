using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBaseTab : UIBase
{
    [SerializeField]
    private ToggleGroup toggleGroup;
    [SerializeField]
    private Transform pageContent;

    protected Toggle[] toggles;
    protected UIBase[] tabs;
    protected int m_iCurPage;

    protected override void Init()
    {
        base.Init();
        InitToggles();
        InitTabs();
    }

    private void InitToggles()
    {
        if(toggleGroup != null)
           toggles = toggleGroup.GetComponentsInChildren<Toggle>();
        else
        {
            Debug.LogError("You must set a toggleGroup First");
        }
        if(toggles != null)
        {
            for (int i = 0; i < toggles.Length; ++i)
            {
                int index = i;
                toggles[i].onValueChanged.AddListener((isOn) =>
                {
                    if (isOn)
                    {
                        OnSelect(index);
                    }
                });
            }
        }
    }

    private void InitTabs()
    {
        if (pageContent != null)
            tabs = pageContent.GetComponentsInChildren<UIBase>();
        else
        {
            Debug.LogError("You must set a PageContent First");
        }
    }

    private void OnSelect(int index)
    {
        if (m_iCurPage == index)
            return;
        m_iCurPage = index;
        for (int i = 0;i < tabs.Length;i++)
        {
            if(index == i)
            {
                tabs[i].Show();
            }
            else
            {
                tabs[i].Hide();
            }
        }
    }

    public virtual void SelectItem(int index)
    {
        if(index >= 0 && index < toggles.Length)
        {
            if(toggles[index].isOn)
               toggles[index].isOn = false;
            toggles[index].isOn = true;
        }
    }

    protected override void DeInit()
    {
        base.DeInit();
        if (toggles != null)
        {
            for (int i = 0; i < toggles.Length; ++i)
            {
                int index = i;
                toggles[i].onValueChanged.RemoveAllListeners();
            }
        }
    }

}
