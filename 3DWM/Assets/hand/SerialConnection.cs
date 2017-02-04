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
    
    ~SerialConnection()
    {
        if (serialPort_.IsOpen)
        {
            serialPort_.Close();
            if (!serialPort_.IsOpen) Debug.Log("Serial connection was closed");
        }
        else
        {
            Debug.Log("Serial connection had already been closed");
        }
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

    public string readRead()
    {
        return message_;
    }

    public void Read()
    {
        Debug.Log("read");
        while (serialPort_ != null && serialPort_.IsOpen)
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
        }
        Debug.Log("Read function was finished");
        return;
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