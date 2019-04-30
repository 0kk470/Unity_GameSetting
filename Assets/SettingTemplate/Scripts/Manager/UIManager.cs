using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

class UIManager:Singleton<UIManager>
{
    private Canvas m_UIRoot;
    private EventSystem m_EventSystem;
    private Dictionary<string, UIBase> m_UIdic = new Dictionary<string, UIBase>();
    public override void Init()
    {
        m_UIRoot = GameObject.Find("Canvas").GetComponent<Canvas>();
        m_EventSystem = GameObject.FindGameObjectWithTag("EventSystem").GetComponent<EventSystem>();
        UnityEngine.Object.DontDestroyOnLoad(m_UIRoot.gameObject);
        UnityEngine.Object.DontDestroyOnLoad(m_EventSystem.gameObject);
    }

    public override void DeInit()
    {
        base.DeInit();
    }

    public UIBase OpenUI(string name,bool bShow = true)
    {
        UIBase ui;
        if(m_UIdic.TryGetValue(name,out ui))
        {
            if(bShow)
               ui.Show();
        }
        else
        {
            string path = null;
            if(Config.UIConfig.TryGetValue(name,out path))
            {
                var go = Resources.Load(path) as GameObject;
                if (go != null)
                {
                    var uiObj = GameObject.Instantiate(go,m_UIRoot.transform, false);
                    ui = uiObj.GetComponent<UIBase>();
                    ui.uiName = name;
                    m_UIdic.Add(name, ui);
                }
                if(bShow)
                {
                    ui.Show();
                }
                else
                {
                    ui.Hide();
                }
            }
            else
            {
                Debug.LogError("WrongConfig,UIName:" + name);
            }
        }
        return ui;
    }

    public UIBase GetUI(string name)
    {
        UIBase ui = null;
        if ( !m_UIdic.TryGetValue(name,out ui) )
        {
            Debug.LogErrorFormat("Error, < {0} > UI exists,but the object reference is null",name);
        }
        return ui;
    }

    public void CloseUI(string name)
    {
        if(m_UIdic.ContainsKey(name))
        {
            var ui = m_UIdic[name];
            RemoveUI(name);
            if (!ui.IsNull())
                ui.Close();
        }
    }

    public void RemoveUI(string name)
    {
        if (string.IsNullOrEmpty(name))
            return;
        if(m_UIdic.ContainsKey(name))
        {
            m_UIdic.Remove(name);
        }
    }
}
