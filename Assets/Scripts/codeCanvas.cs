using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class codeCanvas : MonoBehaviour
{
    public float delaySecond = 1;
    public string nameScene = "level2";
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
}
