using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script will be used to detect the player location relative to camera width
//The goal is to get the player's location as a percent of the screen 
//(0 == very left of the screen)->(1 == very right of the screen)
public class DetectPlayerOnCamera : MonoBehaviour {
	private float p_screenPercent;
	private Transform player;
	private Camera cam;

	void Start(){
		player = GameObject.FindWithTag("Player").transform;
		cam = GetComponent<Camera>();
	}

	// Update is called once per frame
	void Update () {
		//Track player position & set p_screenPercent
		Vector3 screenPos = cam.WorldToScreenPoint(player.position);
        //Debug.Log("target is " + screenPos.x + " pixels from the left");
		p_screenPercent = screenPos.x / cam.pixelWidth;
		//Debug.Log(p_screenPercent);
	}

	//returns player position as a percent of the left side of the screen from 0 to 1 
	public float GetPlayerLocation(){
		return p_screenPercent;
	}
}
