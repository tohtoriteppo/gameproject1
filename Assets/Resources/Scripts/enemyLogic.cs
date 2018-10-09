using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyLogic : MonoBehaviour {

    public int prize;
    public int swingRange = 0;
    public float movingSpeed = 0;
    public Animator animator;
    public bool hit = false;

    private float moveAmount = 0.01f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(transform.position.x - movingSpeed * moveAmount, transform.position.y, transform.position.z);
        if(hit)
        {
            animator.SetBool("hit", true);
            movingSpeed = 0;
        }
        if (transform.position.x < Camera.main.transform.position.x - 2)
        {
            Destroy(gameObject);
        }
    }
}
