using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class SettingManager:Singleton<SettingManager>
{
    private List<Resolution> m_Resolutions;

    private ISetting[] settings;

    //VideoSetting
    public Vec2Data screen;
    public BoolData isFullScreen;
    public BoolData vSync;
    public IntData  lockFrame;

    public List<Resolution> supportedResolutions
    {
        get
        {
            if(m_Resolutions == null)
            {
                m_Resolutions = new List<Resolution>(Screen.resolutions);
            }
            return m_Resolutions;
        }
    }

    public override void Init()
    {

        settings = new ISetting[]
        {
            screen         = new Vec2Data("ScreenSize",new Vector2(1280,720)),
            isFullScreen   = new BoolData("FullScreen",true),
            vSync          = new BoolData("vSync",false),
            lockFrame      = new IntData("lockFrame",60),
        };
        Debug.Log("Setting Manager Init");
        LoadAll();
    }

    public override void DeInit()
    {
        base.DeInit();
    }

    public void SaveAll()
    {
        if(settings != null)
        {
            for (int i = 0; i < settings.Length; i++)
                settings[i].Save();
        }
        PlayerPrefs.Save();
    }

    public void LoadAll()
    {
        if (settings != null)
        {
            for (int i = 0; i < settings.Length; i++)
                settings[i].LoadConfig();
        }
    }

    public void ApplyVideoSetting()
    {
        Screen.SetResolution((int)screen.value.x, (int)screen.value.y, isFullScreen.value);
        Application.targetFrameRate = lockFrame.value;
        QualitySettings.vSyncCount = vSync.value? 1 : 0;
    }
}
