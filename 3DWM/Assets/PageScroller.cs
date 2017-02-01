using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices; // for DllImport

public class PageScroller : MonoBehaviour {
    [DllImport("USER32.dll", CallingConvention = CallingConvention.StdCall)]
    static extern void SetCursorPos(int X, int Y);
    [DllImport("USER32.dll", CallingConvention = CallingConvention.StdCall)]
    static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

    private const int MOUSEEVENTF_WHEEL = 0x0800;
    public GameObject display;
    public GameObject index_finger;

    bool isScrolling = false;
    bool pageDown = false;
    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        if (isScrolling)
        {
            // Tracking the index finger
            Vector3 mouse_pos = display.transform.InverseTransformPoint(index_finger.transform.position);
            int x = (int)(1920 * (5.2 + mouse_pos.x) / 10.4);
            int y = (int)(1080 * (5.3 - mouse_pos.y) / 10.6);
            SetCursorPos(x, y);
            int diff = 12 * (pageDown ? -1 : 1);
            mouse_event(MOUSEEVENTF_WHEEL, 0, 0, diff, 0);                // マウスの左ボタンアップイベントを発生させる
        }
    }

    public void StartScrollDown()
    {
        Debug.Log("*** Start Scrorll Down !!! ***");
        isScrolling = true;
        pageDown = true;
    }

    public void StartScrollUp()
    {
        Debug.Log("*** Start Scrorll Up !!! ***");
        isScrolling = true;
        pageDown = false;
    }

    public void StopScroll()
    {
        Debug.Log("*** Stop Scrorll !!! ***");
        isScrolling = false;
    }
}
