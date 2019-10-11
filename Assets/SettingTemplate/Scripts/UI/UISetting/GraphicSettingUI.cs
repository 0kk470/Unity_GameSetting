using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphicSettingUI : UIBase {

    private SimpleOptionControl m_GraphicLevelOption;
    private SimpleOptionControl m_TextureQualityOption;
    private SimpleOptionControl m_AnisotropicFilteringOption;
    private SimpleOptionControl m_ShadowsOption;
    private SimpleOptionControl m_ShadowResolutionOption;
    private Slider              m_ShadowDistanceSlider;
    private Toggle              m_SoftParticleToggle;

    protected override void Init()
    {
        base.Init();
        m_GraphicLevelOption = transform.Find("Scroll View/Viewport/Content/graphicLevelOption").GetComponent<SimpleOptionControl>();
        m_TextureQualityOption = transform.Find("Scroll View/Viewport/Content/textureQualityOption").GetComponent<SimpleOptionControl>();
        m_AnisotropicFilteringOption = transform.Find("Scroll View/Viewport/Content/anisotropicFilteringOption").GetComponent<SimpleOptionControl>();
        m_ShadowsOption = transform.Find("Scroll View/Viewport/Content/shadowQualityOption").GetComponent<SimpleOptionControl>();
        m_ShadowResolutionOption = transform.Find("Scroll View/Viewport/Content/shadowResolutionOption").GetComponent<SimpleOptionControl>();
        m_ShadowDistanceSlider = transform.Find("Scroll View/Viewport/Content/shadowDistanceSlider/Slider").GetComponent<Slider>();
        m_SoftParticleToggle = transform.Find("Scroll View/Viewport/Content/softParticleToggle/Toggle").GetComponent<Toggle>();

        m_GraphicLevelOption.InitOptions(eGraphicLevel.Low.GetEnumNameList());
        m_TextureQualityOption.InitOptions(eTextureQuality.Low.GetEnumNameList());
        m_AnisotropicFilteringOption.InitOptions(AnisotropicFiltering.Disable.GetEnumNameList());
        m_ShadowsOption.InitOptions(ShadowQuality.Disable.GetEnumNameList());
        m_ShadowResolutionOption.InitOptions(ShadowResolution.Low.GetEnumNameList());

        LoadConfig();

        m_GraphicLevelOption.onOptionSelect.AddListener(OnGraphicLevelSelect);
        m_TextureQualityOption.onOptionSelect.AddListener(OnTextureQualitySelect);
        m_AnisotropicFilteringOption.onOptionSelect.AddListener(OnAnisotropicFilteringSelect);
        m_ShadowsOption.onOptionSelect.AddListener(OnShadowQualitySelect);
        m_ShadowResolutionOption.onOptionSelect.AddListener(OnShadowResolutionSelect);
        m_ShadowDistanceSlider.onValueChanged.AddListener(OnShadowDistanceChanged);
        m_SoftParticleToggle.onValueChanged.AddListener(OnSoftParticleChanged);
    }

    protected override void DeInit()
    {
        base.DeInit();
    }

    protected override void LoadConfig()
    {
        base.LoadConfig();
        m_GraphicLevelOption.value = SettingManager.Instance.st_GraphicLevel;
        m_TextureQualityOption.value = SettingManager.Instance.st_TextureQuality;
        m_AnisotropicFilteringOption.value = SettingManager.Instance.st_AnisotropicFiltering;
        m_ShadowsOption.value = SettingManager.Instance.st_Shadows;
        m_ShadowResolutionOption.value = SettingManager.Instance.st_ShadowResolution;
        m_ShadowDistanceSlider.value = SettingManager.Instance.st_ShadowDistance;
        m_SoftParticleToggle.isOn = SettingManager.Instance.st_SoftParticle;
    }

    public override void Refresh()
    {
        base.Refresh();
    }

    protected override void OnShow()
    {
        EventManager.Instance.AddListener(GameEvent.OnGraphicLevelChanged, OnGraphicLevelChanged);
        base.OnShow();
    }

    protected override void OnHide()
    {
        EventManager.Instance.RemoveListener(GameEvent.OnGraphicLevelChanged, OnGraphicLevelChanged);
        base.OnHide();
    }

    private void OnGraphicLevelChanged(object data)
    {
        LoadConfig();
    }

    private void OnGraphicLevelSelect(int index)
    {
        SettingManager.Instance.SetGraphicLevel((eGraphicLevel)index);
    }

    private void OnTextureQualitySelect(int index)
    {
        SettingManager.Instance.SetTextureQuality((eTextureQuality)index);
    }

    private void OnAnisotropicFilteringSelect(int index)
    {
        SettingManager.Instance.SetAnisotropicFiltering((AnisotropicFiltering)index);
    }

    private void OnShadowQualitySelect(int index)
    {
        ShadowQuality quality = (ShadowQuality)index;
        SettingManager.Instance.SetShadow(quality);

        m_ShadowDistanceSlider.gameObject.SetActiveEx(quality != ShadowQuality.Disable);
        m_ShadowResolutionOption.gameObject.SetActiveEx(quality != ShadowQuality.Disable);
    }

    private void OnShadowResolutionSelect(int index)
    {
        SettingManager.Instance.SetShadowResolution((ShadowResolution)index);
    }

    private void OnShadowDistanceChanged(float distance)
    {
        SettingManager.Instance.SetShadowDistance(Mathf.FloorToInt(distance));
    }

    private void OnSoftParticleChanged(bool isOn)
    {
        SettingManager.Instance.SetSoftParticle(isOn);
    }
}
