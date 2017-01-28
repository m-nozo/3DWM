using UnityEngine;
using System;

using System.Collections;
using System.Runtime.InteropServices; // for DllImport

public class DeviceHandler : MonoBehaviour
{
    [DllImport("USER32.dll", CallingConvention = CallingConvention.StdCall)]
    static extern void SetCursorPos(int X, int Y);
    [DllImport("USER32.dll", CallingConvention = CallingConvention.StdCall)]
    static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);
    private const int MOUSEEVENTF_LEFTDOWN = 0x2;
    private const int MOUSEEVENTF_LEFTUP = 0x4;

    public SerialConnection sc;
    void Start()
    {
        sc = new SerialConnection();  // OpenConnection
    }
    // hit thumb
    public void Hit_thumb()
    {
        sc.Write("a\0");
        Invoke("fin_thumb", 1f);
    }
    void fin_thumb()
    {
        sc.Write("v\0");
    }

    // hit index
    public void Hit_index()
    {
        sc.Write("b\0");
        Invoke("fin_index", 1f);
    }
    void fin_index()
    {
        sc.Write("w\0");
    }

    // hit middle
    public void Hit_middle()
    {
        sc.Write("c\0");
        Invoke("fin_middle", 1f);
    }
    void fin_middle()
    {
        sc.Write("x\0");
    }

    // hit ring
    public void Hit_ring()
    {
        sc.Write("d\0");
        Invoke("fin_ring", 1f);
    }
    void fin_ring()
    {
        sc.Write("y\0");
    }

    // hit pinky
    public void Hit_pinky()
    {
        sc.Write("e\0");
        Invoke("fin_pinky", 1f);
    }
    void fin_pinky()
    {
        sc.Write("z\0");
    }

    void OnTriggerStay(Collider other) {
        click_tester(other);
        if (this.gameObject.name == "bone3LT")
        {
            Debug.Log("hit thumb");
            Hit_thumb();
        }
        if (this.gameObject.name == "bone3LI")
        {
            Debug.Log("hit index");
            Hit_index();
        }
        if (this.gameObject.name == "bone3LM")
        {
            Debug.Log("hit middle");
            Hit_middle();
        }
        if (this.gameObject.name == "bone3LR")
        {
            Debug.Log("hit ring");
            Hit_ring();
        }
        if (this.gameObject.name == "bone3LP")
        {
            Debug.Log("hit pinky");
            Hit_pinky();
        }
    }
    
    // click event test
    private void click_tester(Collider other)
    {
        // ヒットした指とディスプレイの相対ベクトル
        Vector3 mouse_pos = other.transform.InverseTransformPoint(this.transform.position);
        Debug.Log(mouse_pos);
        
        // var point = button2.Parent.PointToScreen(button2.Location); // button2の座標取得
        // var point = Point(100, 200);
        SetCursorPos((int)(1920 * (5.2 + mouse_pos.x)/10.4), (int)(1080 * (5.3 - mouse_pos.y) / 10.6));
        mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);              // マウスの左ボタンダウンイベントを発生させる
        mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);                // マウスの左ボタンアップイベントを発生させる
    }
}
