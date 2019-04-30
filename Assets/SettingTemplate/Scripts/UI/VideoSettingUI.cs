using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class VideoSettingUI : UIBase,ISettingUI {

    private Toggle   m_FullScreenToggle;
    private Toggle   m_VsyncToggle;
    private Dropdown m_ResolutionDropdown;
    private Slider   m_LockFrameSlider;

    public bool isSettingChanged
    {
        get
        {
            return SettingManager.Instance.isSettingChange(SettingType.Video);
        }
    }

    protected override void Init()
    {
        base.Init();
        Debug.Log("VideoSetting Init");
        m_FullScreenToggle = transform.Find("fullscreenToggle").GetComponent<Toggle>();
        m_VsyncToggle = transform.Find("vsyncToggle").GetComponent<Toggle>();
        m_ResolutionDropdown = transform.Find("resolutionDropdown").GetComponent<Dropdown>();
        m_LockFrameSlider = transform.Find("lockFrameSlider").GetComponent<Slider>();

        LoadConfig();

        m_FullScreenToggle.onValueChanged.AddListener(OnFullScreenToggleChange);
        m_VsyncToggle.onValueChanged.AddListener(OnVsyncToggleClick);
        m_ResolutionDropdown.onValueChanged.AddListener(OnResolutionSelect);
        m_LockFrameSlider.onValueChanged.AddListener(OnLockFrameValueChanged);
    }

    protected override void LoadConfig()
    {
        m_FullScreenToggle.isOn = SettingManager.Instance.st_IsFullScreen;
        m_VsyncToggle.isOn = SettingManager.Instance.st_VSync;
        m_LockFrameSlider.value = SettingManager.Instance.st_lockFrame;
        LoadDropdown();
    }

    public override void Refresh()
    {
        base.Refresh();
        LoadConfig();
    }

    private void LoadDropdown()
    {
        m_ResolutionDropdown.ClearOptions();
        var resolutions = SettingManager.Instance.supportedResolutions;
        int iSelectIndex = 0;
        Vector2 screenSize = SettingManager.Instance.st_screen;
        int refreshRate = SettingManager.Instance.st_RefreshRate;
        for (int i = 0;i < resolutions.Count;i++)
        {
            if (refreshRate == resolutions[i].refreshRate && screenSize.x == resolutions[i].width && screenSize.y == resolutions[i].height)
                iSelectIndex = i;
            m_ResolutionDropdown.options.Add(new Dropdown.OptionData(resolutions[i].ToString()));
        }
        m_ResolutionDropdown.value = iSelectIndex;
        m_ResolutionDropdown.RefreshShownValue();
    }

    private void OnFullScreenToggleChange(bool isOn)
    {
        SettingManager.Instance.st_IsFullScreen.Set(isOn);
    }

    private void OnVsyncToggleClick(bool isOn)
    {
        SettingManager.Instance.SetVsync(isOn);
    }

    private void OnResolutionSelect(int iSelectIndex)
    {
        var resolutions = SettingManager.Instance.supportedResolutions;
        if(iSelectIndex < resolutions.Count)
        {
            SettingManager.Instance.st_screen.Set(Vector2.right * resolutions[iSelectIndex].width + Vector2.up * resolutions[iSelectIndex].height);
            SettingManager.Instance.st_RefreshRate.Set(resolutions[iSelectIndex].refreshRate);
        }
    }

    private void OnLockFrameValueChanged(float fValue)
    {
        int iValue = Mathf.FloorToInt(fValue);
        SettingManager.Instance.SetFrameLock(iValue);
    }

    private void OnSettingDataChange(EventData data)
    {
        LoadConfig();
    }

    public void ApplySetting()
    {
        SettingManager.Instance.ApplySetting(SettingType.Video);
    }

    public void RevertSetting()
    {
        SettingManager.Instance.RevertSetting(SettingType.Video);
    }

    protected override void OnShow()
    {

    }

    protected override void OnHide()
    {
        if(isSettingChanged)
        {
            MessageBox.ShowMessage(Config.VideoSettingNotSaveTip, ApplySetting, RevertSetting);
        }
    }
}
