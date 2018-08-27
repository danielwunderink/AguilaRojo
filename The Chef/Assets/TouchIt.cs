using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchIt : MonoBehaviour {

    int count;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        count++;
        if (count%60 == 0) {
            //Debug.Log("60 Frames completed.");
        }
	}
}
