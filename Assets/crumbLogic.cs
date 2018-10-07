using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crumbLogic : MonoBehaviour {

    private float speed;
    private int angle;
    private float spinSpeed;
    private float gravity;
    private float ySpeed;
    private float xSpeed;
    public int amount;
    private bool stopped = false;
    // Use this for initialization
    void Start () {
        speed = Random.Range(0.03f, 0.06f);
        angle = Random.Range(15, 55);
        spinSpeed = Random.Range(0.01f, 0.06f);
        gravity = 0.001f;
        ySpeed = Mathf.Sin((angle*Mathf.PI)/180)*speed;
        xSpeed = Mathf.Cos((angle * Mathf.PI) / 180) * speed;
        Debug.Log(speed);
        Debug.Log(angle);
        Debug.Log(spinSpeed);
        Debug.Log(gravity);
        Debug.Log(ySpeed);
        Debug.Log(xSpeed);
    }
	
	// Update is called once per frame
	void Update () {
        if(!stopped)
        {
            ySpeed -= gravity;
            //Debug.Log(ySpeed);
            transform.position = new Vector3(transform.position.x + xSpeed, transform.position.y + ySpeed, transform.position.z);
            if (transform.position.y < -1.6f)
            {
                stopped = true;
                GetComponent<BoxCollider2D>().enabled = true;
                Debug.Log("HWAAT");
            }
        }
        if(transform.position.x < Camera.main.GetComponent<gameController>().wolf.transform.position.x)
        {
            Camera.main.GetComponent<gameController>().updateFoodAmount(amount);
            Destroy(gameObject);
        }
       
	}

    }
