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
        get;
        set;
    }

    protected override void Init()
    {
        isSettingChanged = false;
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
        m_FullScreenToggle.isOn = SettingManager.Instance.isFullScreen.value;
        m_VsyncToggle.isOn = SettingManager.Instance.vSync.value;
        m_LockFrameSlider.value = SettingManager.Instance.lockFrame.value;
        LoadDropdown();
    }

    private void LoadDropdown()
    {
        m_ResolutionDropdown.ClearOptions();
        var resolutions = SettingManager.Instance.supportedResolutions;
        int iSelectIndex = 0;
        for(int i = 0;i < resolutions.Count;i++)
        {
            if (SettingManager.Instance.screen.value.x == resolutions[i].width && SettingManager.Instance.screen.value.y == resolutions[i].height)
                iSelectIndex = i;
            m_ResolutionDropdown.options.Add(new Dropdown.OptionData(resolutions[i].ToString()));
        }
        m_ResolutionDropdown.value = iSelectIndex;
        m_ResolutionDropdown.RefreshShownValue();
    }

    private void OnFullScreenToggleChange(bool isOn)
    {
        SettingManager.Instance.isFullScreen.value = isOn;
    }

    private void OnVsyncToggleClick(bool isOn)
    {
        SettingManager.Instance.vSync.value = isOn;
    }

    private void OnResolutionSelect(int iSelectIndex)
    {
        var resolutions = SettingManager.Instance.supportedResolutions;
        if(iSelectIndex < resolutions.Count)
        {
            SettingManager.Instance.screen.value = new Vector2(resolutions[iSelectIndex].width, resolutions[iSelectIndex].height);
        }
    }

    private void OnLockFrameValueChanged(float fValue)
    {
        int iValue = Mathf.FloorToInt(fValue);
        SettingManager.Instance.lockFrame.value = iValue;
    }

    public void ApplySetting()
    {
        SettingManager.Instance.ApplyVideoSetting();
    }
}
