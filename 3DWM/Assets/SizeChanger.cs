using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeChanger : MonoBehaviour {
    public GameObject display;
    public GameObject pair;
    Rigidbody src_rb, dst_rb;

    // Use this for initialization
    void Start () {
        src_rb = GetComponent<Rigidbody>();
        dst_rb = pair.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {
        float dist = Vector3.Distance(src_rb.position, dst_rb.position);
        display.transform.localScale = new Vector3(dist, dist, dist);
    }
}
