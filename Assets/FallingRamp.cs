using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRamp : MonoBehaviour {

    private Rigidbody2D bodyRef;
    private Vector3 movementVec = new Vector3(0, -13, 0);
    private bool fall;

	// Use this for initialization
	void Start () {
        bodyRef = gameObject.GetComponent<Rigidbody2D>();
        fall = false;
	}
	
	// Update is called once per frame
	void Update () {
        if(fall)
        {
            bodyRef.velocity = movementVec;
        }
	}

    public void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            Invoke("setFall", .3f); //fall after a moment
        }
    }

    public void setFall()
    {
        fall = true;
    }


}
