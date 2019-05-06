using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SimpleOptionControl : UIBehaviour {

    public class OptionSelectEvent : UnityEvent<int>
    {
        public OptionSelectEvent() { }
    }


    [SerializeField]
    private Button m_PrevBtn;
    public  Button prevBtn { get { return m_PrevBtn; } set { m_PrevBtn = value; } }

    [SerializeField]
    private Button m_NextBtn;
    public  Button nextBtn { get { return m_NextBtn; } set { m_NextBtn = value; } }

    [SerializeField]
    private Text   m_Caption;
    public  Text   caption { get { return m_Caption; } set { m_Caption = value; } }

    [SerializeField]
    private List<string> m_Options = new List<string>();

    private int m_iCurOption = 0;

    public int value
    {
        get { return m_iCurOption; }
        set { SelectOption(value); }
    }

    public OptionSelectEvent onOptionSelect = new OptionSelectEvent();

    protected override void Awake()
    {
        m_PrevBtn.onClick.AddListener(OnPrevBtnClick);
        m_NextBtn.onClick.AddListener(OnNextBtnClick);
        base.Awake();
    }

    protected override void OnDestroy()
    {
        ClearOptions();
        m_PrevBtn.onClick.RemoveListener(OnPrevBtnClick);
        m_NextBtn.onClick.RemoveListener(OnNextBtnClick);
        if (onOptionSelect != null)
            onOptionSelect.RemoveAllListeners();
        base.OnDestroy();
    }

#if UNITY_EDITOR
    protected override void OnValidate()
    {
        base.OnValidate();
        RefreshCaption();
    }
#endif

    public void InitOptions(List<string> newOptions)
    {
        ClearOptions();
        AddOptions(newOptions);
    }

    public void AddOption(string data)
    {
        if(m_Options != null)
        {
            m_Options.Add(data);
        }
    }

    public void AddOptions(List<string> newOptions)
    {
        if (newOptions == null)
            return;
        m_Options.AddRange(newOptions);
    }

    public void ClearOptions()
    {
        m_iCurOption = 0;
        m_Options.Clear();
        m_Caption.text = string.Empty;
    }

    public int GetOptionCount()
    {
        int iCount = 0;
        if (m_Options != null)
            iCount = m_Options.Count;
        return iCount;
    }

    public void SelectOption(int index)
    {
        if (index == m_iCurOption)
            return;
        if (index >= 0 && index < m_Options.Count)
        {
            m_iCurOption = index;
            m_Caption.text = m_Options[index];
            if(onOptionSelect != null)
            {
                onOptionSelect.Invoke(index);
            }
        }
        else
        {
            Debug.LogWarning("Invalid option index for SimpleOption:" + index);
        }
    }

    public void RefreshCaption()
    {
        if (m_Caption != null && m_Options != null)
        {
            if (m_iCurOption >= 0 && m_iCurOption < m_Options.Count)
                m_Caption.text = m_Options[m_iCurOption];
        }
    }

    private void OnPrevBtnClick()
    {
        if (m_iCurOption <= 0)
            return;
        SelectOption(m_iCurOption - 1);
    }

    private void OnNextBtnClick()
    {
        if (m_iCurOption >= m_Options.Count - 1)
            return;
        SelectOption(m_iCurOption + 1);
    }
}
