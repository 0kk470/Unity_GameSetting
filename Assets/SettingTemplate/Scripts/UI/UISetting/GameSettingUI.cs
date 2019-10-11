using UnityEngine.UI;
using UnityEngine;
public sealed class GameSettingUI : UIBaseTab
{
    private Button m_ApplyBtn;
    private Button m_CloseBtn;

    protected override void Init()
    {
        base.Init();
        m_ApplyBtn = transform.Find("Buttons/apply_btn").GetComponent<Button>();
        m_CloseBtn = transform.Find("Buttons/close_btn").GetComponent<Button>();
        m_ApplyBtn.onClick.AddListener(OnApplyBtnClick);
        m_CloseBtn.onClick.AddListener(OnCloseBtnClick);
        SelectItem(0);
        onSelectChanged += OnSelectPageChanged;
    }

    private void OnCloseBtnClick()
    {
        Hide();
    }

    public void OnApplyBtnClick()
    {
        if(m_iCurPage >=0 && m_iCurPage < childsUI.Length)
        {
            ISettingUI settingUI = childsUI[m_iCurPage] as ISettingUI;
            if(settingUI != null)
            {
                Debug.Log(childsUI[m_iCurPage].gameObject.name + " Apply");
                settingUI.ApplySetting();
            }
        }
    }

    private void OnSelectPageChanged(int index)
    {
        //Apply Setting Btn is only visible for VideoSetting
        m_ApplyBtn.gameObject.SetActiveEx(index == 0);
    }

    protected override void DeInit()
    {
        base.DeInit();
        onSelectChanged -= OnSelectPageChanged;
        m_ApplyBtn.onClick.RemoveListener(OnApplyBtnClick);
        m_CloseBtn.onClick.RemoveListener(OnCloseBtnClick);
    }
}
