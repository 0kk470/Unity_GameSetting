using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        Init();
        UIManager.Instance.OpenUI("GameSettingUI");
	}

    void Init()
    {
        DontDestroyOnLoad(gameObject);
        SettingManager.Instance.Init();
        UIManager.Instance.Init();
        EventManager.Instance.Init();
    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(UIManager.Instance.IsUIOpen("GameSettingUI"))
            {
                UIManager.Instance.CloseUI("GameSettingUI");
            }
            else
            {
                UIManager.Instance.OpenUI("GameSettingUI");
            }
        }
    }

    void DeInit()
    {
        SettingManager.Instance.DeInit();
        UIManager.Instance.DeInit();
        EventManager.Instance.DeInit();
    }

    private void OnDestroy()
    {
        DeInit();
    }

    private void OnApplicationQuit()
    {
        SettingManager.Instance.SaveSetting();
    }

}
