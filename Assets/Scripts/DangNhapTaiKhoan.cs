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
            print("Kết nối không thành công");
        }
        else if (www.isDone)
        {
            string get = www.downloadHandler.text;
            if (get == "empty")
            {
                thongbao.text = "Vui lòng nhập đầy đủ thông tin đăng nhập";
            }
            else if (get == "" || get == null)
            {
                thongbao.text = "Tài khoản hoặc mật khẩu không chính xác";
            }
            else if (get.Contains("Lỗi"))
            {
                thongbao.text = "Không kết nối được tới server";
            }
            else
            {
                thongbao.text = "Đăng nhập thành công";
                PlayerPrefs.SetString("token", get);
                SceneManager.LoadScene(1);
            }
        }


    }
}
