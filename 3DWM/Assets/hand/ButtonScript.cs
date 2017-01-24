using UnityEngine;
using System.Collections;
using System.IO.Ports;
using System.Threading;

public class ButtonScript
{
    public delegate void SerialDataReceivedEventHandler(string message);
    
    private static SerialPort serialPort_ = new SerialPort("COM3", 9600);
    
    // Use this for initialization
    public ButtonScript()
    {
        if (!serialPort_.IsOpen)
            OpenConnection();
    }

    void OpenConnection()
    {
        if (serialPort_ != null)
        {
            if (serialPort_.IsOpen)
            {
                serialPort_.Close();
                Debug.LogError("Failed to open Serial Port, already open!");
            }
            else
            {
                serialPort_.Open();
                serialPort_.ReadTimeout = 50;
                Debug.Log("Open Serial port");
            }
        }
        Debug.Log("open");
    }
    //void Awake()
    //{
    //    Open();
    //}
    
    void OnApplicationQuit()
    {
        serialPort_.Close();
    }

    public void Write(string message)
    {
        try
        {
            serialPort_.Write(message);
        }
        catch (System.Exception e)
        {
            Debug.LogWarning(e.Message);
        }
    }
}