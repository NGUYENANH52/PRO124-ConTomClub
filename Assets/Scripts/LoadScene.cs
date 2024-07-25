using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public void DangNhap()
    {
        SceneManager.LoadScene(2);
    }
    public void QuayLai()
    {
        SceneManager.LoadScene(0);
    }
    public void DangKy()
    {
        SceneManager.LoadScene(3);
    }
    public void ChoiNgay()
    {
        SceneManager.LoadScene(1);
    }
    public void CaiDat()
    {
        SceneManager.LoadScene(4);
    }
}
