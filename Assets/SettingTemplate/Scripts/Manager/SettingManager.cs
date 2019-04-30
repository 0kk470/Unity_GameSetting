using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public enum SettingType
{
    Video,
    Audio,
    Graphic,
    Game,
    Input,
    All,
}

class SettingManager : Singleton<SettingManager>
{
    private List<Resolution> m_Resolutions;

    //Setting Interface
    private ISettingData[] videoSettings;
    private ISettingData[] audioSettings;
    private ISettingData[] graphicSettings;
    private ISettingData[] gameSettings;
    private ISettingData[] inputSettings;

    //VideoSettingData
    public Vec2Data st_screen;
    public BoolData st_IsFullScreen;
    public BoolData st_VSync;
    public IntData  st_lockFrame;
    public IntData  st_RefreshRate;
    public List<Resolution> supportedResolutions
    {
        get
        {
            if (m_Resolutions == null)
            {
                m_Resolutions = new List<Resolution>(Screen.resolutions);
            }
            return m_Resolutions;
        }
    }

    public override void Init()
    {
        Debug.Log("Setting Manager Init");

        videoSettings = new ISettingData[]
        {
            st_screen       = new Vec2Data("ScreenSize",new Vector2(1280,720)),
            st_IsFullScreen = new BoolData("FullScreen",true),
            st_VSync        = new BoolData("vSync",false),
            st_lockFrame    = new IntData ("lockFrame",60),
            st_RefreshRate  = new IntData ("RefreshRate",60),
        };

        LoadSetting();
        ApplySetting(SettingType.Video);
    }

    public override void DeInit()
    {
        base.DeInit();
    }


    private void SaveHelper(ISettingData[] settings)
    {
        if (settings != null)
        {
            for (int i = 0; i < settings.Length; i++)
                settings[i].Save();
        }
    }

    private void LoadHelper(ISettingData[] settings)
    {
        if (settings != null)
        {
            for (int i = 0; i < settings.Length; i++)
                settings[i].LoadConfig();
        }
    }

    private void RevertHelper(ISettingData[] settings)
    {
        if (settings != null)
        {
            for (int i = 0; i < settings.Length; i++)
                settings[i].Revert();
        }
    }

    public void SaveSetting(SettingType type = SettingType.All)
    {
        switch (type)
        {
            case SettingType.Video:
                {
                    SaveHelper(videoSettings);
                    break;
                }
            case SettingType.Audio:
                {
                    SaveHelper(audioSettings);
                    break;
                }
            case SettingType.Graphic:
                {
                    SaveHelper(graphicSettings);
                    break;
                }
            case SettingType.Game:
                {
                    SaveHelper(gameSettings);
                    break;
                }
            case SettingType.Input:
                {
                    SaveHelper(inputSettings);
                    break;
                }
            case SettingType.All:
                {
                    SaveHelper(videoSettings);
                    SaveHelper(audioSettings);
                    SaveHelper(graphicSettings);
                    SaveHelper(gameSettings);
                    SaveHelper(inputSettings);
                    break;
                }
        }
        PlayerPrefs.Save();
    }

    public void LoadSetting(SettingType type = SettingType.All)
    {
        switch (type)
        {
            case SettingType.Video:
                {
                    LoadHelper(videoSettings);
                    break;
                }
            case SettingType.Audio:
                {
                    LoadHelper(audioSettings);
                    break;
                }
            case SettingType.Graphic:
                {
                    LoadHelper(graphicSettings);
                    break;
                }
            case SettingType.Game:
                {
                    LoadHelper(gameSettings);
                    break;
                }
            case SettingType.Input:
                {
                    LoadHelper(inputSettings);
                    break;
                }
            case SettingType.All:
                {
                    LoadHelper(videoSettings);
                    LoadHelper(audioSettings);
                    LoadHelper(graphicSettings);
                    LoadHelper(gameSettings);
                    LoadHelper(inputSettings);
                    break;
                }
        }
    }

    public void RevertSetting(SettingType type = SettingType.All)
    {
        switch (type)
        {
            case SettingType.Video:
                {
                    RevertHelper(videoSettings);
                    break;
                }
            case SettingType.Audio:
                {
                    RevertHelper(audioSettings);
                    break;
                }
            case SettingType.Graphic:
                {
                    RevertHelper(graphicSettings);
                    break;
                }
            case SettingType.Game:
                {
                    RevertHelper(gameSettings);
                    break;
                }
            case SettingType.Input:
                {
                    RevertHelper(inputSettings);
                    break;
                }
            case SettingType.All:
                {
                    RevertHelper(videoSettings);
                    RevertHelper(audioSettings);
                    RevertHelper(graphicSettings);
                    RevertHelper(gameSettings);
                    RevertHelper(inputSettings);
                    break;
                }
        }
        ApplySetting(type,false);
    }

    public bool isSettingChange(SettingType type)
    {
        bool isChanged = false;
        switch (type)
        {
            case SettingType.Video:
                {
                    isChanged = st_screen.isChanged || st_IsFullScreen.isChanged;
                    break;
                }
            case SettingType.Audio:
                {
                    break;
                }
            case SettingType.Graphic:
                {
                    break;
                }
            case SettingType.Game:
                {
                    break;
                }
            case SettingType.Input:
                {
                    break;
                }
        }
        return isChanged;
    }

    public void ApplySetting(SettingType type, bool bSaveNow = true)
    {
        switch (type)
        {
            case SettingType.Video:
                {
                    Vector2 screenSize = st_screen;
                    SetResolution((int)screenSize.x, (int)screenSize.y, st_IsFullScreen,st_RefreshRate);
                    SetFrameLock(st_lockFrame);
                    SetVsync(st_VSync);
                    break;
                }
            case SettingType.Audio:
                {
                    break;
                }
            case SettingType.Graphic:
                {
                    break;
                }
            case SettingType.Game:
                {
                    break;
                }
            case SettingType.Input:
                {
                    break;
                }
            case SettingType.All:
                {
                    break;
                }
        }
        if (bSaveNow)
            SaveSetting(type);
    }

    //Apply Setting Interface Start

    //Video
    public void SetResolution(int width = 0, int height = 0, bool isFullScreen = true, int refreshRate = 60)
    {
        width = width == 0 ? Screen.width : width;
        height = height == 0 ? Screen.height : height;
        Screen.SetResolution(width, height, isFullScreen, refreshRate);
    }

    public void SetFrameLock(int iFrame, bool bSave = true)
    {
        Application.targetFrameRate = iFrame;
        if (bSave)
        {
            st_lockFrame.Set(iFrame);
            st_lockFrame.Save();
        }
    }

    public void SetVsync(bool isOn, bool bSave = true)
    {
        QualitySettings.vSyncCount = isOn ? 1 : 0;
        if (bSave)
        {
            st_VSync.Set(isOn);
            st_VSync.Save();
        }

    }

    //Apply Setting Interface End
}
