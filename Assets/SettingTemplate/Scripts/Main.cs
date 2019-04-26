using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        DontDestroyOnLoad(gameObject);
        SettingManager.Instance.Init();
        UIManager.Instance.Init();
        UIManager.Instance.OpenUI("GameSettingUI");
	}

    private void OnDestroy()
    {
        UIManager.Instance.DeInit();
    }

    private void OnApplicationQuit()
    {
        SettingManager.Instance.SaveAll();
    }

}
