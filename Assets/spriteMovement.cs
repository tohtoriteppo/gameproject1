using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spriteMovement : MonoBehaviour {

    public float moveSpeed;
    private int num = 4;
    private float yPos = 0.7f;
    // Use this for initialization
    void Start () {
        transform.position = new Vector3(transform.position.x, transform.position.y + yPos * moveSpeed - 0.4f, transform.position.z+ num * moveSpeed);
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(transform.position.x + moveSpeed * 0.01f, transform.position.y, transform.position.z);
        if(transform.position.x < Camera.main.transform.position.x-2)
        {
            Destroy(gameObject);
        }
	}
}
