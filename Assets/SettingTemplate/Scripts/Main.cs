﻿using System.Collections;
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
        InputManager.Instance.Init();
    }


    void Update()
    {
        InputManager.Instance.Update();
    }

    void DeInit()
    {
        SettingManager.Instance.DeInit();
        UIManager.Instance.DeInit();
        EventManager.Instance.DeInit();
        InputManager.Instance.DeInit();
    }

    private void OnDestroy()
    {
        DeInit();
    }

    private void OnApplicationQuit()
    {
        SettingManager.Instance.SaveSetting();
    }

    struct testdata { public string name; }
    
    [ContextMenu("Test")]
    void Test()
    {
        testdata data = new testdata();
        data.name = "test";
        Debug.Log(data.name);
    }
}
