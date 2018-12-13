using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjBehav : MonoBehaviour {

    //just some variables for movement
    private bool movingRight;
    private Vector3 velocityVec;
    private float speedMult;
    private float lifeSpan;
    public bool playerFriendly; //if we want to resuse this for enemies, set this bool for enemies
    private Rigidbody2D bodyRef;

	// Use this for initialization
	void Start () {
        bodyRef = gameObject.GetComponent<Rigidbody2D>();
        lifeSpan = 2;
    }
	
	// Update is called once per frame
	void Update () {
        velocityVec = new Vector3(0,0,0);
        lifeSpan -= Time.deltaTime;
        if(lifeSpan < 0)
        {
            Destroy(this.gameObject);
        }
		if(movingRight) //if we want our projectile to go right
        {
            velocityVec += new Vector3(100,0,0) * speedMult;
        }
        else //otherwise we want it to go left
        {
            velocityVec += new Vector3(-100, 0, 0) * speedMult;
        }
        bodyRef.velocity = velocityVec;
	}

    public void setup(bool moveRight, bool player, float speed, float life)
    {
        movingRight = moveRight;
        speedMult = speed;
        playerFriendly = player;
        life = lifeSpan;
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if(playerFriendly)
        {
            if(col.gameObject.name == "SnakeEnemy")
            {
                Destroy(col.gameObject); //kill snake with fire
                Destroy(this.gameObject); //kill fire with snake
            }
        }
    }
}
