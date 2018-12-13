using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wrapRun : MonoBehaviour {
    public float camera_left;
    public float camera_right;
    public float movement_multiplier;
    public bool dropBlock;
    public bool triggerOncePerCycle;
	// Use this for initialization
	void Start () {
        triggerOncePerCycle = true;
        dropBlock = false;
        //These values assume the game is in fullscreen
        camera_left = -15f;
        camera_right = 15f;
        movement_multiplier = 15f;
        transform.position = new Vector3(camera_left, transform.position.y, transform.position.z);
    }
	
	// Update is called once per frame
	void Update () {
        float temp = transform.position.x + Time.deltaTime * movement_multiplier;
        transform.position = new Vector3(temp, transform.position.y, transform.position.z);
        if(temp >= -5.4 && triggerOncePerCycle)
        {
            dropBlock = true;
            triggerOncePerCycle = false;
        }
        //if outside the right side of the camera
        if (temp > camera_right)
        {
            transform.position = new Vector3(camera_left, transform.position.y, transform.position.z);
            dropBlock = false;
            triggerOncePerCycle = true;
        }
    }
}
