using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript : MonoBehaviour {

    public bool triggered;

	// Use this for initialization
	void Start () {
        triggered = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player" || col.gameObject.tag == "proj")
        {
            SpriteRenderer m_SpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            m_SpriteRenderer.color = Color.green;
            triggered = true;
        }
    }
}
