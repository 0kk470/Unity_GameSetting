using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public static class Extesnsion
{
    public static void SetActiveEx(this GameObject go,bool bActive)
    {
        if(go != null && go.activeSelf != bActive)
        {
            go.SetActive(bActive);
        }
    }
}
