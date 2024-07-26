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
    public void Trangchu()
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
    public void ThongTinNguoiChoi()
    {
        SceneManager.LoadScene(5);
    }
    public void ThongTinNhanVat()
    {
        SceneManager.LoadScene(8);
    }
    public void ShopVatPham()
    {
        SceneManager.LoadScene(6);
    }
    public void ShopUpgrade()
    {
        SceneManager.LoadScene(7);
    }
}
