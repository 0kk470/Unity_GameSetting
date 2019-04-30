using UnityEngine.UI;
using UnityEngine;
public sealed class GameSettingUI : UIBaseTab
{
    private Button m_ApplyBtn;

    protected override void Init()
    {
        base.Init();
        m_ApplyBtn = transform.Find("Buttons/apply_btn").GetComponent<Button>();
        m_ApplyBtn.onClick.AddListener(OnApplyBtnClick);
        SelectItem(0);
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

    protected override void DeInit()
    {
        base.DeInit();
        m_ApplyBtn.onClick.RemoveListener(OnApplyBtnClick);
    }
}
