using UnityEngine;
using System;

using System.Collections;
using System.Runtime.InteropServices; // for DllImport

public class SerialReceiver : MonoBehaviour
{
    [DllImport("USER32.dll", CallingConvention = CallingConvention.StdCall)]
    static extern void SetCursorPos(int X, int Y);
    [DllImport("USER32.dll", CallingConvention = CallingConvention.StdCall)]
    static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);
    private const int MOUSEEVENTF_LEFTDOWN = 0x2;
    private const int MOUSEEVENTF_LEFTUP = 0x4;

    public ButtonScript sp;
    void Start()
    {
        sp = new ButtonScript();
    }
    // Update is called once per frame
    public void ButtonPush()
    {
        sp.Write("a\0");
        //System.Threading.Thread.Sleep(300);
        Invoke("fin", 1f);
    }

    void fin()
    {
        sp.Write("v\0");
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("hit 0");
        click_tester();
        ButtonPush();
    }

    // click event test
    private void click_tester()
    {
        Debug.Log(this.gameObject.name);
        // var point = button2.Parent.PointToScreen(button2.Location); // button2の座標取得
        // var point = Point(100, 200);
        SetCursorPos(100, 150);   // button2へ移動
        mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);              // マウスの左ボタンダウンイベントを発生させる
        mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);                // マウスの左ボタンアップイベントを発生させる
    }
}
