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
    private const int MOUSEEVENTF_MOVE = 0x0001;
    private const int MOUSEEVENTF_ABSOLUTE = 0x8000;
    public SerialConnection sc;
    public GameObject display;

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

    void OnTriggerEnter(Collider other)
    {
        FireLeftClickDown(other);
        if (this.gameObject.name == "bone3LI")
        {
            Debug.Log("Enter: index");
            //Hit_index();
        }
        if (this.gameObject.name == "IndexBall")
        {
            Debug.Log("hit indexball!!!!");
            Hit_index();
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        //FireLeftClickUp(other);
        if (this.gameObject.name == "bone3LT")
        {
            Debug.Log("hit thumb");
            Hit_thumb();
        }
        if (this.gameObject.name == "bone3LI")
        {
            Debug.Log("hit index");
            //Hit_index();
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
        if (this.gameObject.name == "IndexBall")
        {
            Debug.Log("hit indexball!!!!");
            Hit_index();
        }
    }
    
    void Update()
    {
        // Tracking the index finger
        Vector3 mouse_pos = display.transform.InverseTransformPoint(this.transform.position);
        int x = (int)(1920 * (5.2 + mouse_pos.x) / 10.4);
        int y = (int)(1080 * (5.3 - mouse_pos.y) / 10.6);
        SetCursorPos(x, y);
        mouse_event(MOUSEEVENTF_MOVE, 0, 0, 0, 0);

        //sc.Read();
    }
    
    private void FireLeftClickDown(Collider other)
    {
        // ヒットした指とディスプレイの相対ベクトル
        Vector3 mouse_pos = other.transform.InverseTransformPoint(this.transform.position);
        Debug.Log((int)(1920 * (5.2 + mouse_pos.x) / 10.4)+ ", " +(int)(1080 * (5.3 - mouse_pos.y) / 10.6));

        int x = (int)(1920 * (5.2 + mouse_pos.x) / 10.4);
        int y = (int)(1080 * (5.3 - mouse_pos.y) / 10.6);
        SetCursorPos(x, y);
        mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);              // マウスの左ボタンダウンイベントを発生させる
        mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);                // マウスの左ボタンアップイベントを発生させる
    }

    private void FireLeftClickUp(Collider other)
    {
        // ヒットした指とディスプレイの相対ベクトル
        Vector3 mouse_pos = other.transform.InverseTransformPoint(this.transform.position);
        Debug.Log((int)(1920 * (5.2 + mouse_pos.x) / 10.4) + ", " + (int)(1080 * (5.3 - mouse_pos.y) / 10.6));

        int x = (int)(1920 * (5.2 + mouse_pos.x) / 10.4);
        int y = (int)(1080 * (5.3 - mouse_pos.y) / 10.6);
        SetCursorPos(x, y);
        mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);                // マウスの左ボタンアップイベントを発生させる
    }
}
