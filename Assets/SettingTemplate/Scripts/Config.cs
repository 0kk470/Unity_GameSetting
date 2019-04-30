using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Config
{
    //<UIName,UIPrefabPath>
    public static Dictionary<string, string> UIConfig = new Dictionary<string, string>()
    {
        { "GameSettingUI", "Prefab/UI/GameSettingUI" },
        { "MessageBox"   , "Prefab/UI/MessageBox"  },
    };
    //Text
    public const string MessageBoxBtn1Name = "Confirm";
    public const string MessageBoxBtn2Name = "Cancel";
    public const string VideoSettingNotSaveTip = "You have unsaved changes on Video Setting \n Do you want to apply these changes?";
}
