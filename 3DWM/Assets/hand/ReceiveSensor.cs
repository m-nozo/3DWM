using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class ReceiveSensor : MonoBehaviour {

    public SerialConnection so;
    private string mess;
    // Use this for initialization
    void Start()
    {
        so = new SerialConnection();
        Debug.Log("out open");
    }
    void Update()
    {
        mess = so.readRead();
        Debug.Log("get value : " + mess);
    }
}
