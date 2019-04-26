using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIBaseTab : UIBase
{
    [SerializeField]
    private ToggleGroup toggleGroup;
    [SerializeField]
    private Transform pageContent;

    protected Toggle[] toggles;
    protected int m_iCurPage;

    public UnityAction<int> onSelectChanged;

    protected override void Init()
    {
        base.Init();
        InitToggles();
        InitChildUI();
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

    private void InitChildUI()
    {
        if (pageContent != null)
            childsUI = pageContent.GetComponentsInChildren<UIBase>();
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
        for (int i = 0;i < childsUI.Length;i++)
        {
            if(index == i)
            {
                childsUI[i].Show();
            }
            else
            {
                childsUI[i].Hide();
            }
        }
        if(onSelectChanged != null)
        {
            onSelectChanged.Invoke(index);
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
        if (toggles != null)
        {
            for (int i = 0; i < toggles.Length; ++i)
            {
                toggles[i].onValueChanged.RemoveAllListeners();
            }
        }
        base.DeInit();
    }

}
