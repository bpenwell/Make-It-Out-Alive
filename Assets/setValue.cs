using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class setValue : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Scene me;
        me = SceneManager.GetActiveScene();
        if (me.name == "Level1")
        {
            GetComponent<Text>().text = PlayerPrefs.GetInt("lvl1Deaths").ToString();

        }
        else if(me.name == "Level2")
        {
            GetComponent<Text>().text = PlayerPrefs.GetInt("lvl2Deaths").ToString();
            
        }
    }
}
