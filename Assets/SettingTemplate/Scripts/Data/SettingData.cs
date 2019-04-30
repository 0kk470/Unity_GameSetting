using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface ISettingData
{
    void Save();
    void Revert();
    void LoadConfig();
}

public abstract class SettingData<T>: ISettingData
{
    protected T m_Data;
    public string key
    {
        get;
        private set;
    }

    public SettingData(string _key, T _data)
    {
        OnInit();
        key = _key;
        m_Data = _data;
    }

    public void Set(T data)
    {
        m_Data = data;
    }

    public virtual void OnInit() { }

    public virtual void Save() { }

    public virtual void LoadConfig() { }

    public virtual bool isChanged { get { return false; } }

    public virtual void Revert()
    {
        LoadConfig();
    }
}

public class IntData : SettingData<int>
{

    public IntData(string _key, int _data) : base(_key, _data) { }

    public override void OnInit()
    {
        base.OnInit();
        m_Data = 0;
    }

    public override void LoadConfig()
    {
        m_Data = PlayerPrefs.GetInt(key, m_Data);
    }

    public override void Save()
    {
        PlayerPrefs.SetInt(key, m_Data);
    }

    public override bool isChanged
    {
        get
        {
            return m_Data != PlayerPrefs.GetInt(key, m_Data);
        }
    }

    public static implicit operator int(IntData data)
    {
        return data.m_Data;
    }
}

public class FloatData:SettingData<float>
{

    public FloatData(string _key, float _data) : base(_key, _data) { }

    public override void OnInit()
    {
        base.OnInit();
        m_Data = 0;
    }

    public override void LoadConfig()
    {
        m_Data = PlayerPrefs.GetFloat(key, m_Data);
    }

    public override void Save()
    {
        PlayerPrefs.SetFloat(key, m_Data);
    }

    public override bool isChanged
    {
        get
        {
            return m_Data != PlayerPrefs.GetFloat(key, m_Data);
        }
    }

    public static implicit operator float(FloatData data)
    {
        return data.m_Data;
    }
}

public class BoolData : SettingData<bool>
{

    public BoolData(string _key, bool _data) : base(_key, _data) { }

    public override void OnInit()
    {
        base.OnInit();
        m_Data = false;
    }

    public override void LoadConfig()
    {
        m_Data = PlayerPrefs.GetInt(key, 0) == 1;
    }

    public override void Save()
    {
        PlayerPrefs.SetInt(key, m_Data?1:0);
    }

    public override bool isChanged
    {
        get
        {
            return m_Data != ( PlayerPrefs.GetInt(key,0) == 1 );
        }
    }

    public static implicit operator bool(BoolData data)
    {
        return data.m_Data;
    }
}

public class StringData : SettingData<string>
{

    public StringData(string _key, string _data) : base(_key, _data) { }

    public override void OnInit()
    {
        base.OnInit();
        m_Data = string.Empty;
    }

    public override void LoadConfig()
    {
        m_Data = PlayerPrefs.GetString(key, m_Data);
    }

    public override void Save()
    {
        PlayerPrefs.SetString(key, m_Data);
    }

    public override bool isChanged
    {
        get
        {
            return m_Data != PlayerPrefs.GetString(key, m_Data);
        }
    }

    public static implicit operator string(StringData data)
    {
        return data.m_Data;
    }
}

public class Vec2Data : SettingData<Vector2>
{

    public Vec2Data(string _key, Vector2 _data) : base(_key, _data) { }

    public override void OnInit()
    {
        base.OnInit();
        m_Data = Vector2.zero;
    }

    public override void LoadConfig()
    {
        var result = PlayerPrefs.GetString(key);
        if(!string.IsNullOrEmpty(result))
        {
            m_Data = JsonUtility.FromJson<Vector2>(result);
        }
    }

    public override void Save()
    {
        PlayerPrefs.SetString(key, JsonUtility.ToJson(m_Data));
    }

    public override bool isChanged
    {
        get
        {
            var Vect2 = m_Data;
            var result = PlayerPrefs.GetString(key);
            if (!string.IsNullOrEmpty(result))
            {
                Vect2 = JsonUtility.FromJson<Vector2>(result);
            }
            return m_Data != Vect2;
        }
    }

    public static implicit operator Vector2(Vec2Data data)
    {
        return data.m_Data;
    }
}
