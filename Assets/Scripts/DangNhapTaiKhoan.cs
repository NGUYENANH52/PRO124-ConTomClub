using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class DangNhapTaiKhoan : MonoBehaviour
{
    public TMP_InputField taikhoan;
    public TMP_InputField matkhau;
    public TextMeshProUGUI thongbao;

    public void DangNhapXacNhan()
    {
        StartCoroutine(DangNhap());
    }

    private IEnumerator DangNhap()
    {
        WWWForm form = new WWWForm();
        form.AddField("user", taikhoan.text);
        form.AddField("passwd", matkhau.text);

        UnityWebRequest www = UnityWebRequest.Post("https://fpl.expvn.com/dangnhap.php", form);
        yield return www.SendWebRequest();

        if (!www.isDone)
        {
            print("Connection failed");
        }
        else if (www.isDone)
        {
            string get = www.downloadHandler.text;
            if (get == "empty")
            {
                thongbao.text = "Account or Password cannot be empty";
            }
            else if (get == "" || get == null)
            {
                thongbao.text = "Account or Password is incorrect";
            }
            else if (get.Contains("Lỗi"))
            {
                thongbao.text = "Unable to connect to the server";
            }
            else
            {              
                PlayerPrefs.SetString("token", get);
                SceneManager.LoadScene("GameMenu");
            }
        }


    }
}
