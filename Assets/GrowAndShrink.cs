using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowAndShrink : MonoBehaviour {

    public float minScale;
    public float maxScale;
    public float growMultiplier;
    private bool switchDirection = false;
	// Update is called once per frame
	void Update () {
        if (switchDirection)
        {
            transform.localScale += new Vector3(Time.deltaTime* growMultiplier, Time.deltaTime* growMultiplier, 0);
            if(transform.localScale.x >= maxScale)
            {
                switchDirection = false;
            }
        }
        else
        {
            transform.localScale -= new Vector3(Time.deltaTime* growMultiplier, Time.deltaTime* growMultiplier, 0);
            if (transform.localScale.x <= minScale)
            {
                switchDirection = true;
            }
        }
	}
}
