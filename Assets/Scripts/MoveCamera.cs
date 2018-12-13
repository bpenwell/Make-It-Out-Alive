using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour {

	public float MovementMultiplier = 10f;
	public Transform startLocation;
	public Transform endLocation;
    public bool isPlaying;


    private float x_StartValue;
    private float percentDone;
    // Use this for initialization
    void Start () {
		//This is a var that should range from 0-1, where 0 is the beginning of the level, and 1 is the end
		//We will use this percentage to control the camera movementspeed
		//We will derive this value off the camera position relative to startLocation and endLocation
		percentDone = 0;
        x_StartValue = transform.position.x;
        isPlaying = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (isPlaying)
        {
            x_StartValue += Time.deltaTime * MovementMultiplier;
            transform.position = new Vector3(x_StartValue, transform.position.y, transform.position.z);

        }
	}
}
