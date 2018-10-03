using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour {

    public float moveSpeed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(transform.position.x+moveSpeed*0.01f, transform.position.y, transform.position.z);
        Camera.main.transform.position = new Vector3(transform.position.x+1f, transform.position.y+0.4f, Camera.main.transform.position.z);
        GetComponent<swing>().weapon.transform.position = new Vector3(transform.position.x, transform.position.y, GetComponent<swing>().weapon.transform.position.z);

    }
}
