using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour {

    private bool hiding;
    private bool following;
    private float targetHeight;
    public GameObject detector;
    private Rigidbody2D bodyRef;
    private Vector3 movementVec;

    public GameObject bossProj;
    private float projCoolDown;

	// Use this for initialization
	void Start () {
        hiding = true;
        following = false;
        targetHeight = 45;
        bodyRef = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        movementVec = new Vector3(0, 0, 0);
		if(hiding && detector.GetComponent<DetectPlayer>().playerDetected)
        {
            hiding = false;
        }
        else if(!hiding && !following)//rise
        {
            if(gameObject.transform.position.y < targetHeight)
            {
                movementVec += new Vector3(26, 15, 0);
            }
            else
            {
                following = true;
                projCoolDown = .4f;
            }
        }
        else if(following)
        {
            projCoolDown -= Time.deltaTime;
            movementVec += new Vector3(26, 0, 0);
            if(projCoolDown <= 0)
            {
                Vector3 newPos = gameObject.transform.position;
                newPos.x += 100;
                newPos.y += -targetHeight + Random.Range(5f,20f);
                GameObject proj = Instantiate(bossProj, newPos, Quaternion.identity);
                proj.GetComponent<ProjBehav>().setup(false, false, .6f, .4f);
                proj.GetComponent<SpriteRenderer>().color = Color.red;
                projCoolDown = 1f;
            }
        }
        bodyRef.velocity = movementVec;
	}
}
