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
    Rigidbody dst_rb, ang_rb;
    public Vector3 center = new Vector3(0f, 0f, -1000f);

    // Use this for initialization
    void Start ()
    {
        so = new SerialConnection();
        Debug.Log("instantiated an SerialConnection object");
        dst_rb = index.GetComponent<Rigidbody>();
        //Debug.Log(dst_rb.transform.position);
    }
	
	// Update is called once per frame
	void LateUpdate ()
    {
        mess = so.readRead();
        try
        {
            bend_v = float.Parse(mess);
            //Debug.Log(bend_v);
            bend_v = 500 - bend_v;
            prev_bend_v = bend_v / 5000;
        }
        catch {
        }
        dst_rb.transform.localPosition = new Vector3(-0.025f,  - prev_bend_v, -0.117f + (prev_bend_v / (float)(2.2)));
    }
}
