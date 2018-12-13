using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script should distribute the data of the player to the environment
//If the player is in the red zone or burnout zone, the environment should be notified through this script
public class DataDistributor : MonoBehaviour {
	public DetectPlayerOnCamera m_playerTracker;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//If the player is in the red zone
		if(m_playerTracker.GetPlayerLocation() <= .1f){
			Debug.Log("IN RED ZONE");
		}

		//If the player is in the burnout zone
		if(m_playerTracker.GetPlayerLocation() >= .75f){
			Debug.Log("IN BURNOUT ZONE");
		}
	}
}
