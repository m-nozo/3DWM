using UnityEngine;
using System.Collections;
using System.IO.Ports;
using System.Threading;

public class SerialConnection 
{
    public delegate void SerialDataReceivedEventHandler(string message);
    private static SerialPort serialPort_ = new SerialPort("COM3", 9600);
    public static string message_;
    private Thread thread_;
    public bool GetMessage_;

    // Use this for initialization
    public SerialConnection()
    {
        if (!serialPort_.IsOpen)
            OpenConnection();

        GetMessage_ = false;
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
        thread_ = new Thread(Read);
        thread_.Start();
        Debug.Log("open");
    }

    void OnApplicationQuit()
    {
        serialPort_.Close();
        Debug.Log("hogohogo");
        //if (thread_ != null && thread_.IsAlive)
        {
            thread_.Join();
        }
        Debug.Log("kkkkk");

    }

    public string readRead()
    {
        return message_;
    }

    public void Read()
    {
        int i = 0; // for debug
        Debug.Log("read");
        while (serialPort_ != null && serialPort_.IsOpen /*&& i < 1000*/)
        {
            try
            {
                {
                    message_ = serialPort_.ReadLine();
                    GetMessage_ = true;
                }
            }
            catch (System.Exception e)
            {
                Debug.LogWarning(e.Message);
            }
            i++;
        }
        Debug.Log("join");
        thread_.Join();
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