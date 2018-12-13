using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Use this class to apply camera shaking for whenever we want to shake the camera for whatever reason
public class ShakeCamera : MonoBehaviour {

    public Transform camTran; //ref for camera transform

    //mechanical stuff
    public float shakeDur = 0f; //how long the camera shakes for
    public float shakeAmp = .5f; //how hard the camera shakes for

    private Vector3 origPos = new Vector3(0f, 0f, -10f); //the position of the camera before it is shaken

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		if (shakeDur <= 0) //means that the camera is not actively shaking
        {
            origPos = camTran.localPosition;
            shakeDur = 0f;
        }
        else //means we should be shaking the camera
        {
            Vector3 temp = new Vector3(camTran.localPosition.x, origPos.y + Random.insideUnitSphere.y * shakeAmp, origPos.z + Random.insideUnitSphere.y * shakeAmp); //Unity makes me make a temp variable because it's racist
            camTran.localPosition = temp;
            shakeDur -= Time.deltaTime;
        }
	}

    //use this function to call a camera shake. can set how long and how hard the shake should be
    public void callShake(float newDur, float newAmp)
    {
        shakeDur = newDur;
        shakeAmp = newAmp;
    }
}
