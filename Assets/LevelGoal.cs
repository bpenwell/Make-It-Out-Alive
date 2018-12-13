using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGoal : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTriggerStay2D(Collider2D col)
    {
        if(col.tag == "Player") //next level
        {

            Invoke("changeLevel", 1.5f);
        }
    }

    public void changeLevel()
    {
        if (SceneManager.GetActiveScene().name == "Level1") //Add on as needed
        {
            SceneManager.LoadScene("Level2");
        }
        else
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
