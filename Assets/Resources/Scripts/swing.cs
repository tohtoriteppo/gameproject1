using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class swing : MonoBehaviour {

    private bool animation;
    private int swingSpeed = 5;
    private int currentAngle = 0;
    private int swingCD = 60;
    private int animationCounter = 0;
    private int whenToSwing;
    private List<GameObject> ObjectsInRange = new List<GameObject>();

    public GameObject food;

    public float swingDelay;

    // Use this for initialization
    void Start () {
        //animation = false;	

        whenToSwing = (int)(swingDelay*swingCD);
        Debug.Log("whenswing "+ whenToSwing);

    }
	
	// Update is called once per frame
	void Update () {
        if (animationCounter >= swingCD && Input.GetButtonDown("Fire1"))
        {
            //animation = true;
            Debug.Log("Swing");
            animationCounter = 0;
        }
        if(animationCounter < swingCD)
        {

            //Debug.Log("Swing");
            //int newAngle = currentAngle + swingSpeed;
            //currentAngle = newAngle >= 360 ? 0 : newAngle;
            //transform.rotation = Quaternion.Euler(0, 0, currentAngle);
            animationCounter++;
        }
        if(animationCounter == whenToSwing)
        {
            foreach(GameObject obj in ObjectsInRange)
            {
                Debug.Log(obj.tag);
                if (obj.tag == "enemy")
                {
                    //Debug.Log("EENEMY HITT");
                    Color tmp = obj.GetComponent<SpriteRenderer>().color;
                    tmp.a = 0.5f;
                    obj.GetComponent<SpriteRenderer>().color = tmp;
                    food.GetComponent<Text>().text = (int.Parse(food.GetComponent<Text>().text) + obj.GetComponent<enemyLogic>().prize).ToString();
                    //Change prize to 0????
                    //obj.GetComponent<enemyLogic>().prize = 0;
                }
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log("enemy detected");
        ObjectsInRange.Add(col.gameObject);
    }
    public void OnTriggerExit2D(Collider2D col)
    {
       // Debug.Log("enemy left");
        //Probably you'll have to calculate wich object it is
        ObjectsInRange.Remove(col.gameObject);
    }
}
