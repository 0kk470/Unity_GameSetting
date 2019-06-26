using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum UILayer
{
    Bottom = 1,
    Middle = 10,
    Top = 50,
    Tip = 100,
}

class UIManager:Singleton<UIManager>
{
    private Canvas m_UIRoot;
    private EventSystem m_EventSystem;
    private Dictionary<string, UIBase> m_UIdic = new Dictionary<string, UIBase>();
    private Dictionary<UILayer, Transform> m_layers = new Dictionary<UILayer, Transform>();
    public override void Init()
    {
        m_UIRoot = GameObject.Find("Canvas").GetComponent<Canvas>();
        m_EventSystem = GameObject.FindGameObjectWithTag("EventSystem").GetComponent<EventSystem>();

        InitLayer();

        UnityEngine.Object.DontDestroyOnLoad(m_UIRoot.gameObject);
        UnityEngine.Object.DontDestroyOnLoad(m_EventSystem.gameObject);
    }

    public override void DeInit()
    {
        base.DeInit();
    }

    private void InitLayer()
    {
        m_layers.Clear();
        var layerPrefab = Resources.Load<GameObject>("Prefab/Layer");
        var layerNameList = UILayer.Bottom.GetEnumNameList();
        for(int i = 0;i < layerNameList.Count;i++)
        {
            GameObject go = GameObject.Instantiate(layerPrefab,m_UIRoot.transform,false);
            go.name = layerNameList[i];
            var _canvas = go.GetComponent<Canvas>();
            var gRaycaster = go.GetComponent<GraphicRaycaster>();
            var eLayer = (UILayer)Enum.Parse(typeof(UILayer), layerNameList[i]);
            gRaycaster.SetBlockingMask(LayerMask.GetMask("UI"));
            _canvas.overrideSorting = true;
            _canvas.sortingOrder = (int)eLayer;
            m_layers.Add(eLayer, go.transform);
        }
    }

    public UIBase OpenUI(string name,bool bShow = true,UILayer layer = UILayer.Bottom)
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
                    Transform parent = null;
                    m_layers.TryGetValue(layer, out parent);
                    var uiObj = GameObject.Instantiate(go, parent != null ? parent :m_UIRoot.transform, false);
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

    public bool IsUIOpen(string name)
    {
        if (!m_UIdic.ContainsKey(name))
            return false;
        return m_UIdic[name] != null && m_UIdic[name].IsOpen();
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
