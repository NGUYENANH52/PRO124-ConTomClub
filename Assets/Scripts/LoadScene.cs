using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LoadScene : MonoBehaviour
{       
    public void Trangchu()
    {
        SceneManager.LoadScene(0);
    }
    public void GameMenu()
    {
        SceneManager.LoadScene(1);
    }
    public void PlayScenes()
    {
        SceneManager.LoadScene(2);
    }

    //Exit Game button on UI Toolkit
    public void Exitgame()
    {
        Debug.Log("exitBtn");
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                        Application.Quit();
        #endif
    }
}
