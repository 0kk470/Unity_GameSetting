using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public static class Extesnsion
{
    public static void SetActiveEx(this GameObject go,bool bActive)
    {
        if(go != null && go.activeSelf != bActive)
        {
            go.SetActive(bActive);
        }
    }

    public static bool IsNull(this UnityEngine.Object obj)
    {
        return obj == null || obj.Equals(null);
    }

    public static int ToInt(this Enum e)
    {
        return e.GetHashCode();
    }

    public static List<string> GetEnumNameList(this Enum e)
    {
        return Enum.GetNames(e.GetType()).ToList();
    }

    //put this function in a static class for extension
    public static void SetBlockingMask(this GraphicRaycaster gRaycaster,int maskLayer)
    {
        if (gRaycaster != null)
        {
            var fieldInfo = gRaycaster.GetType().GetField("m_BlockingMask", BindingFlags.NonPublic | BindingFlags.IgnoreCase | BindingFlags.Instance);
            if (fieldInfo != null)
            {
                LayerMask layerMask = new LayerMask();
                layerMask.value = maskLayer;
                fieldInfo.SetValue(gRaycaster, layerMask);
            }
        }
    }
}
