using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class DangKyTaiKhoan : MonoBehaviour
{
    public TMP_InputField taikhoan;
    public TMP_InputField matkhau;
    public TextMeshProUGUI thongbao;

    public void DangKyXacNhan()
    {
        StartCoroutine(DangKy());
    }

    private IEnumerator DangKy()
    {
        WWWForm form = new WWWForm();
        form.AddField("user", taikhoan.text);
        form.AddField("passwd", matkhau.text);

        UnityWebRequest www = UnityWebRequest.Post("https://fpl.expvn.com/dangky.php", form);
        yield return www.SendWebRequest();

        if (!www.isDone)
        {
            print("Kết nối không thành công");
        }
        else if (www.isDone)
        {
            string get = www.downloadHandler.text;
            if (get == "exist")
            {
                thongbao.text = "Tài khoản đã tồn tại";
            }
            else if (get == "ERROR")
            {
                thongbao.text = "Đăng ký không thành công";
            }
            else
            {
                thongbao.text = "Đăng nhập tài khoản thành công";
                SceneManager.LoadScene(0);
            }
            
            
        }       

    }
}
