using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateScript : MonoBehaviour {

    public GameObject trigger; //ref for the trigger
    public float moveDur; //how long it takes the move process
    private Rigidbody2D bodyRef;
    private Vector3 movementVec;
    private bool triggered;

    // Use this for initialization
    void Start () {
        bodyRef = gameObject.GetComponent<Rigidbody2D>();
        moveDur = -1;
        triggered = false;
	}
	
	// Update is called once per frame
	void Update () {
        movementVec = new Vector3(0, 0, 0);
        moveDur -= Time.deltaTime;
        if (moveDur < 0 && trigger.GetComponent<TriggerScript>().triggered  && !triggered)
        {
            moveDur = .6f;
            triggered = true;
        }
        else if (moveDur > .4f && triggered)
        {
            movementVec += new Vector3(0, -5 , 0);
        }
        else if(moveDur < .3f && triggered)
        {
            movementVec += new Vector3(0, 25, 0);
        }
        bodyRef.velocity = movementVec;
	}
}
