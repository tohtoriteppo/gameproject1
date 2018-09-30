using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyLogic : MonoBehaviour {

    public int prize;
    public int swingRange = 0;
    public float movingSpeed = 0;

    private float moveAmount = 0.01f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(transform.position.x - movingSpeed * moveAmount, transform.position.y, transform.position.z);
	}
}
