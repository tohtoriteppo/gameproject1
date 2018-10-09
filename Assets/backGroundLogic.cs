using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backGroundLogic : MonoBehaviour {

    public float moveSpeed;
    private int num = 4;
    private Vector3 startPos;
    private bool move = false;
    // Use this for initialization
    void Start()
    {
        //transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + num * moveSpeed);
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(move)
        {
            transform.position = new Vector3(transform.position.x + moveSpeed * 0.01f, transform.position.y, transform.position.z);
        }
        if (!Camera.main.GetComponent<gameController>().levelStarted)
        {
            move = false;
            transform.position = startPos;
        }
        else
        {
            move = true;
        }
    }
}
