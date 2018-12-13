using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBehav : MonoBehaviour {

    //ref stuff
    public GameObject enProj;
    public DetectPlayer detectRef;
    public Rigidbody2D bodyRef;

    //behav stuff
    private bool isHiding;
    private bool isGrowing;
    private bool isReady;
    private float growRate; //how fast we grow per tick
    private float growMax; //how much we want to grow
    private float currentScale; //how thicc we are
    //private float targetY = 10.15; how much we need to move up to hit to scale properly (used for my quick reference
    private Vector3 movementVec;

	// Use this for initialization
	void Start () {
        isHiding = true;
        isGrowing = false;
        growMax = 10f;
        growRate = 5f;
        currentScale = 1f;
	}
	
	// Update is called once per frame
	void Update () {
        movementVec = new Vector3(0f,0f,0f);
		if(isHiding && detectRef.playerDetected)
        {
            isGrowing = true;
            isHiding = false;
        }
        else if(isGrowing)
        {
            if(currentScale < growMax)
            {
                currentScale += growRate * Time.deltaTime;
                movementVec += new Vector3(0f, 3.425f, 0f); 
                gameObject.transform.localScale = new Vector3(currentScale, currentScale, currentScale);
                
            }
            else if(currentScale >= growMax)
            {
                isGrowing = false;
                isReady = true;
            }
           
        }
        else if(isReady)
        {
            //do nothing for right now
        }
        bodyRef.velocity = movementVec;
    }

}
