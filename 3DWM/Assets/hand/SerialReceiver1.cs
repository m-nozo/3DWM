
using UnityEngine;
using System.Collections;
using System.IO.Ports;

public class SerialReceiver1 : MonoBehaviour {
	private static SerialPort sp = new SerialPort("/dev/tty.usbmodem1421", 9600);

	private string message_;
	// Use this for initialization
	void Start () {
		OpenConnection();
	}

	// Update is called once per frame
	public void ButtonPush() {
		sp.Write("b\0");
		//System.Threading.Thread.Sleep(300);
		Invoke("fin", 1f);
	}

	void fin(){
		sp.Write("w\0");
	}

	private void Read()
	{
			try {
				message_ = sp.ReadLine();

				Debug.Log(message_);
			} catch (System.Exception e) {
				Debug.Log(e.Message);
			}
	}

	void OnApplicationQuit() 
	{
		sp.Close();
	}

	void OpenConnection() {
		if(sp != null) {
			if(sp.IsOpen) {
				sp.Close();
				Debug.LogError("Failed to open Serial Port, already open!");
			} else {
				sp.Open();
				sp.ReadTimeout = 50;
				Debug.Log("Open Serial port");
			}
		}
	}
}
