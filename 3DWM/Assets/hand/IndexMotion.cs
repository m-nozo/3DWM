using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndexMotion : MonoBehaviour {

    public SerialConnection so;
    private string mess;
    private float bend_v;
    private float prev_bend_v;

    public GameObject index;
    public GameObject pair;
    public GameObject angle;
    Rigidbody src_rb, dst_rb, ang_rb;
    public Vector3 center = new Vector3(0f, 0f, -1000f);

    // Use this for initialization
    void Start ()
    {

        so = new SerialConnection();
        Debug.Log("out open");

        dst_rb = index.GetComponent<Rigidbody>();
        src_rb = pair.GetComponent<Rigidbody>();
        //ang_rb = angle.GetComponent<Rigidbody>();
        Debug.Log(dst_rb.transform.position);
        //dst_rb.centerOfMass = center;
    }
	
	// Update is called once per frame
	void LateUpdate ()
    {
        mess = so.readRead();
        try
        {
            bend_v = float.Parse(mess);
            Debug.Log(bend_v);
            bend_v = 500 - bend_v;
            prev_bend_v = bend_v / 5000;
        }
        catch {
            //dst_rb.transform.localPosition = new Vector3(-0.05f, -0.1f - prev_bend_v, -0.1f);
        }


        dst_rb.transform.localPosition = new Vector3(-0.025f,  - prev_bend_v, -0.117f + (prev_bend_v / (float)(2.2)));
        //dst_rb.transform.localPosition = new Vector3(src_rb.position.x + -0.1f, src_rb.position.y + -0.1f, src_rb.position.z + -0.1f);
        //dst_rb.transform.localPosition = new Vector3(-0.1f, -0.1f, -0.1f);
        //dst_rb.transform.eulerAngles = new Vector3(src_rb.transform.eulerAngles.x, src_rb.transform.eulerAngles.y, src_rb.transform.eulerAngles.z);
    }
}
