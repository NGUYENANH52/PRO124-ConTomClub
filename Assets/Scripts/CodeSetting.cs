using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeSetting : MonoBehaviour
{
    [SerializeField]private CanvasGroup Setting;

    public void OpenClosePanel()
    {
        Setting.alpha = Setting.alpha > 0 ? 0 : 1;
        Setting.blocksRaycasts = Setting.blocksRaycasts == true ? false : true;
    }
}
