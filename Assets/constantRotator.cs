using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class constantRotator : MonoBehaviour {

    public float delta = 1.5f;  // Amount to move left and right from the start point
    public float speed = 2.0f;
    public float direction = 1;
    private Quaternion startPos;
    private void Start()
    {
        startPos = transform.rotation;
    }
    // Update is called once per frame
    void Update () {

        Quaternion a = startPos;
        a.z += direction * (delta * Mathf.Sin(Time.time * speed));
        transform.rotation = a;

    }
}