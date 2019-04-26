using UnityEngine.UI;
using System;
class MessageBox:UIBase
{
    private Text   m_Message;
    private Text   m_btn1Name;
    private Text   m_btn2Name;

    private Button m_btn1;
    private Button m_btn2;
    private Button m_closeBtn;
    protected override void Init()
    {
        base.Init();
        m_Message = transform.Find("body/message").GetComponent<Text>();
        m_btn1Name = transform.Find("body/btn1/Text").GetComponent<Text>();
        m_btn2Name = transform.Find("body/btn2/Text").GetComponent<Text>();
        m_btn1 = transform.Find("body/btn1").GetComponent<Button>();
        m_btn2 = transform.Find("body/btn2").GetComponent<Button>();
        m_closeBtn = transform.Find("body/close_btn").GetComponent<Button>();
        m_btn1.onClick.AddListener(OnBtn1Click);
        m_btn2.onClick.AddListener(OnBtn2Click);
        m_closeBtn.onClick.AddListener(OnCloseBtnClick);
    }

    public Action btn1Action;

    public Action btn2Action;

    public static void ShowMessage(string message,Action btn1Callback = null,Action btn2Callback = null,string btn1Name = Config.MessageBoxBtn1Name,string btn2Name = Config.MessageBoxBtn2Name)
    {
        MessageBox mb = UIManager.Instance.OpenUI("MessageBox") as MessageBox;
        if(mb != null)
        {
            mb.btn1Action = btn1Callback;
            mb.btn2Action = btn2Callback;
            mb.SetText(message,btn1Name,btn2Name);
        }
    }

    private void SetText(string message,string btn1Name,string btn2Name)
    {
        m_Message.text = message;
        m_btn1Name.text = btn1Name;
        m_btn2Name.text = btn2Name;
    }

    private void OnBtn1Click()
    {
        if(btn1Action != null)
        {
            btn1Action.Invoke();
        }
        Hide();
    }

    private void OnBtn2Click()
    {
        if (btn2Action != null)
        {
            btn2Action.Invoke();
        }
        Hide();
    }

    private void OnCloseBtnClick()
    {
        Hide();
    }

    protected override void OnHide()
    {
        base.OnHide();
        btn1Action = null;
        btn2Action = null;
    }

    protected override void DeInit()
    {
        btn1Action = null;
        btn2Action = null;
        m_btn1.onClick.RemoveListener(OnBtn1Click);
        m_btn2.onClick.RemoveListener(OnBtn2Click);
        m_closeBtn.onClick.RemoveListener(OnCloseBtnClick);
        base.DeInit();
    }
}
