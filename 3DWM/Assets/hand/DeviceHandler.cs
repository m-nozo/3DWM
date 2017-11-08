using UnityEngine;
using System;

using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices; // for DllImport

public class DeviceHandler : MonoBehaviour
{
    [DllImport("USER32.dll", CallingConvention = CallingConvention.StdCall)]
    static extern void SetCursorPos(int X, int Y);
    [DllImport("USER32.dll", CallingConvention = CallingConvention.StdCall)]
    static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

    [System.Runtime.InteropServices.DllImport("user32.dll")]
    public extern static void SendInput(int nInputs, Input[] pInputs, int cbsize);

    private const int MOUSEEVENTF_LEFTDOWN = 0x2;
    private const int MOUSEEVENTF_LEFTUP = 0x4;
    private const int MOUSEEVENTF_MOVE = 0x0001;
    private const int MOUSEEVENTF_ABSOLUTE = 0x8000;

    static public SerialConnection sc;
    public GameObject display;

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct MouseInput
    {
        public int X;
        public int Y;
        public int Data;
        public int Flags;
        public int Time;
        public int ExtraInfo;
    }



    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit)]
    public struct Input
    {
        [System.Runtime.InteropServices.FieldOffset(0)]
        public int Type;

        [System.Runtime.InteropServices.FieldOffset(4)]
        public MouseInput Mouse;

        /*
        [System.Runtime.InteropServices.FieldOffset(4)]
        public KeyboardInput Keyboard;

        [System.Runtime.InteropServices.FieldOffset(4)]
        public HardwareInput Hardware;
        */

    }

    static public SerialConnection get_sc() { return sc; }

    void Start()
    {
        sc = new SerialConnection();  // OpenConnection
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
    
    void OnTriggerEnter(Collider other)
    {
        FireLeftClickDown(other);
        if (this.gameObject.name == "IndexBall")
        {
            Debug.Log("hit indexball!!!!");
            Hit_index();
        }
    }
    
    void OnTriggerExit(Collider other)
    {
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
    }


    
    private void FireLeftClickDown(Collider other)
    {
        // ヒットした指とディスプレイの相対ベクトル
        Vector3 mouse_pos = other.transform.InverseTransformPoint(this.transform.position);
        Debug.Log((int)(1920 * (5.2 + mouse_pos.x) / 10.4)+ ", " +(int)(1080 * (5.3 - mouse_pos.y) / 10.6));

        int x = (int)(1920 * (5.2 + mouse_pos.x) / 10.4);
        int y = (int)(1080 * (5.3 - mouse_pos.y) / 10.6);

        List<Input> inputs = new List<Input>();
        // Add Mouse Input
        Input input = new Input();
        input.Type = 0; // MOUSE = 0
        input.Mouse.Flags = MOUSEEVENTF_LEFTDOWN | 0x8000 | 0x0001;
        input.Mouse.Data = 0; // ok
        input.Mouse.X = 65535;
        input.Mouse.Y = 65535;
        input.Mouse.Time = 0; // ok
        input.Mouse.ExtraInfo = 0; // ok

        inputs.Add(input);

        SendInput(inputs.ToArray().Length, inputs.ToArray(), System.Runtime.InteropServices.Marshal.SizeOf(inputs[0]));

        Debug.Log("send input called");
        Debug.Log(inputs.ToArray().Length);
        Debug.Log(System.Runtime.InteropServices.Marshal.SizeOf(inputs[0]));
        // SendInput(inputs.Length, inputs, System.Runtime.InteropServices.Marshal.SizeOf(inputs[0]));


        /*
        SetCursorPos(x, y);
        mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);              // マウスの左ボタンダウンイベントを発生させる
        mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);                // マウスの左ボタンアップイベントを発生させる
        */
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
