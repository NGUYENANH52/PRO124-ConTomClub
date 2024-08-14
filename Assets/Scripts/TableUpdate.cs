using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TableUpdate : MonoBehaviour
{
    public GameObject Update;  // Màn hình chính cần cập nhật
    //public GameObject Table;
    // Biến để kiểm tra xem có đang trong màn hình cài đặt hay không
    private bool isInSettings = false;

    public void TogglePauseMenu()
    {
        bool isActive = Update.activeSelf;
        Update.SetActive(!isActive);

        // Chỉ thay đổi Time.timeScale khi không ở trong màn hình cài đặt
        if (!isActive && !isInSettings)
        {
            Time.timeScale = 0f;
        }
/*        else if (!isInSettings)
        {
            Time.timeScale = 1f;
        }*/
    }    
    public void ToggleTable()
    {
        bool isActive = Update.activeSelf;
        Update.SetActive(!isActive);
        if (!isActive)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

}