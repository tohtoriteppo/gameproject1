using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class screenLogic : MonoBehaviour {

    public gameController controller;
	// Use this for initialization
	void Start () {
        controller = Camera.main.GetComponent<gameController>();
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if(Input.GetButtonDown("Fire1"))
        {
            controller.feedBackDone();
        }
	}
}
