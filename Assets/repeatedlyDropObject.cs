using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class repeatedlyDropObject : MonoBehaviour {

    public float floatSpeed;
    public float fallSpeed;

    private bool isDropping;
    private float y_triggerValue;
    private float y_groundValue = 5f;
	// Use this for initialization
	void Start () {
        isDropping = false;

        if(floatSpeed == 0)
        {
            floatSpeed = 3f;
        }
        if(fallSpeed == 0)
        {
            fallSpeed = 5f;
        }
        //Set the height where the block will drop
        y_triggerValue = transform.parent.GetComponent<Transform>().position.y;
	}
	
	// Update is called once per frame
	void Update () {
        if (isDropping)
        {
            float temp = transform.position.y;
            temp -= Time.deltaTime * fallSpeed;
            transform.position = new Vector3(transform.position.x, temp, transform.position.z);
            if (temp <= y_groundValue)
            {
                isDropping = false;
            }
        }
        else
        {
            float temp = transform.position.y;
            temp += Time.deltaTime * floatSpeed;
            transform.position = new Vector3(transform.position.x, temp, transform.position.z);

            //If block has reached the height of trigger, start falling
            if (temp >= y_triggerValue)
            {
                isDropping = true;
            }
        }
        if(Input.GetKey(KeyCode.P)) //Activate easy mode because these blocks suck
        {
            gameObject.tag = "Untagged";
        }
	}
}
