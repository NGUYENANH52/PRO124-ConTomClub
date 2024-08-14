using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TableUpdate : MonoBehaviour
{
    public GameObject Update;
    public void TogglePauseMenu()
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
