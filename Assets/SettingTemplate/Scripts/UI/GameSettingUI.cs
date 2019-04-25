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
    }

    private void OnCloseBtnClick()
    {
        Hide();
    }

    public void OnApplyBtnClick()
    {
        if(m_iCurPage >=0 && m_iCurPage < tabs.Length)
        {
            ISettingUI setting = tabs[m_iCurPage] as ISettingUI;
            if(setting != null)
            {
                Debug.Log(tabs[m_iCurPage].gameObject.name);
                setting.ApplySetting();
            }
        }
    }

    protected override void DeInit()
    {
        base.DeInit();
        m_ApplyBtn.onClick.RemoveListener(OnApplyBtnClick);
    }
}
