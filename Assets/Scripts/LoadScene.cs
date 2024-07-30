using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

}
