using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour {

    public float moveSpeed;
    public GameObject weapon;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(transform.position.x+moveSpeed*0.01f, transform.position.y, transform.position.z);
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y+0.4f, Camera.main.transform.position.z);
        //weapon.transform.position = new Vector3(transform.position.x, transform.position.y, weapon.transform.position.z);

    }
}
