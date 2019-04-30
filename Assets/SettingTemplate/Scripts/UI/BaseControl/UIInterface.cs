

public interface ISettingUI
{
    void ApplySetting();

    void RevertSetting();

    bool isSettingChanged { get; }
}
