using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scenes2 : MonoBehaviour
{
    public float delaySecond = 1;
    public string nameScene = "level2";

    public GameObject popupPause2;
    public Button buttonContinues2;


    //public void ModeSelect()
    //{
    //    StartCoroutine(loadAfterDelay());
    //}
    //IEnumerator loadAfterDelay()
    //{
    //    yield return new WaitForSeconds(delaySecond);
    //    SceneManager.LoadScene(nameScene);
    //}
    public void ShowPopupPause()
    {
        popupPause2.SetActive(true);
        Time.timeScale = 0;
    }

    public void Continues()
    {
        Debug.LogError("OnClickButtonContinues");
        popupPause2.SetActive(false);
        Time.timeScale = 1;
    }
    public void PlayAgain()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
        Time.timeScale = 1;
    }
}

