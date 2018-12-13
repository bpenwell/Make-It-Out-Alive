using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetDeaths : MonoBehaviour {

	// Use this for initialization
	void Start () {
        PlayerPrefs.SetInt("lvl1Deaths", 0);
        PlayerPrefs.SetInt("lvl2Deaths", 0);
    }
}
