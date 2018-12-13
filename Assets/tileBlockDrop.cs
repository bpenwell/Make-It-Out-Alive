using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tileBlockDrop : MonoBehaviour {


    public float floatSpeed;
    public float fallSpeed;
    public wrapRun m_wrapRun;

    private bool isDropping;
    private float y_triggerValue;
    private float y_groundValue = -.5f;
    // Use this for initialization
    void Start()
    {
        isDropping = false;

        if (floatSpeed == 0)
        {
            floatSpeed = 3f;
        }
        if (fallSpeed == 0)
        {
            fallSpeed = 5f;
        }
        //Set the height where the block will drop
        y_triggerValue = transform.parent.GetComponent<Transform>().position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDropping)
        {
            //Debug.Log("Dropping");
            float temp = transform.position.y;
            temp -= Time.deltaTime * fallSpeed;
            transform.position = new Vector3(transform.position.x, temp, transform.position.z);
        }
        else
        {
            //Debug.Log("NOT Dropping");
            float temp = transform.position.y;
            if(temp <= y_triggerValue)
            {
                temp += Time.deltaTime * floatSpeed;
                transform.position = new Vector3(transform.position.x, temp, transform.position.z);
            }

            //If block has reached the height of trigger, start falling
            if (m_wrapRun.dropBlock == true)
            {
                isDropping = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("hitting");
        if(collision.gameObject.tag == "noteGround")
        {
            isDropping = false;
            m_wrapRun.dropBlock = false;
        }
    }
}
