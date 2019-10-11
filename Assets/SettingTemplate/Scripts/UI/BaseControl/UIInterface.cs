using UnityEngine;
using DG.Tweening;

public interface ISettingUI
{
    void ApplySetting();

    void RevertSetting();

    bool isSettingChanged { get; }
}

public interface IAnimationUI
{
    void StartAnimation();

    void StopAnimation();

    void OnAnimationFinish();
}
