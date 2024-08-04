using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scenes : MonoBehaviour
{
    public float delaySecond = 1;
    public string nameScene = "level2";
    public string nameScene2 = "level2";
    public string nameScene3 = "level2";
    public string nameScene4 = "level2";
    public string nameScene5 = "level2";
    public string nameScene6 = "level2";


    public GameObject popupPause;
    public Button buttonContinues;


    public void ModeSelect()
    {
        StartCoroutine(loadAfterDelay());
    }
    IEnumerator loadAfterDelay()
    {
        yield return new WaitForSeconds(delaySecond);
        SceneManager.LoadScene(nameScene);
    }
    //2
    public void ModeSelect2()
    {
        StartCoroutine(loadAfterDelay2());
    }
    IEnumerator loadAfterDelay2()
    {
        yield return new WaitForSeconds(delaySecond);
        SceneManager.LoadScene(nameScene2);
    }
    //3
    public void ModeSelect3()
    {
        StartCoroutine(loadAfterDelay3());
}
IEnumerator loadAfterDelay3()
{
    yield return new WaitForSeconds(delaySecond);
    SceneManager.LoadScene(nameScene3);
}

    //4
    public void ModeSelect4()
    {
        StartCoroutine(loadAfterDelay4());
    }
    IEnumerator loadAfterDelay4()
    {
        yield return new WaitForSeconds(delaySecond);
        SceneManager.LoadScene(nameScene4);
    }
    //5
    public void ModeSelect5()
    {
        StartCoroutine(loadAfterDelay5());
    }
    IEnumerator loadAfterDelay5()
    {
        yield return new WaitForSeconds(delaySecond);
        SceneManager.LoadScene(nameScene5);
    }
    //6
    public void ModeSelect6()
    {
        StartCoroutine(loadAfterDelay6());
    }
    IEnumerator loadAfterDelay6()
    {
        yield return new WaitForSeconds(delaySecond);
        SceneManager.LoadScene(nameScene6);
    }
    public void ShowPopupPause()
    {
        popupPause.SetActive(true);
        Time.timeScale = 0;
    }

    public void Continues()
    {
        Debug.LogError("OnClickButtonContinues");
        popupPause.SetActive(false);
        Time.timeScale = 1;
    }
    public void PlayAgain()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
        Time.timeScale = 1;
    }
}
