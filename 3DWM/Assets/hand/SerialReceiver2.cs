using UnityEngine;
using System.Collections;

public class SerialReceiver2 : MonoBehaviour
{
    public ButtonScript sp;
    public void Start()
    {
        sp = new ButtonScript();
    }
    // Update is called once per frame
    public void ButtonPush()
    {
        sp.Write("c\0");
        //System.Threading.Thread.Sleep(300);
        Invoke("fin", 1f);
    }

    void fin()
    {
        sp.Write("x\0");
    }
    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("hit 2");

        sp.Write("c\0");
        //System.Threading.Thread.Sleep(300);
        Invoke("fin", 1f);
    }
}

