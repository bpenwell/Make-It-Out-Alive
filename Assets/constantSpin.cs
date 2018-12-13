using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class constantSpin : MonoBehaviour {
    public float delta = 1.5f;  // Amount to move left and right from the start point
    public float speed = 2.0f;
    public float direction = 1;
    // Update is called once per frame
    void Update () {
        Quaternion a = transform.rotation;
        a.z += Time.deltaTime*speed;
        transform.rotation = a;

    }
}
