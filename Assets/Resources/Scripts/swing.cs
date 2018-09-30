using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class swing : MonoBehaviour {

    private bool animation;
    private int swingSpeed = 5;
    private int currentAngle = 0;
    private int holdCounter = 0;
    private int stunCounter = 0;
    public int swingCD = 60;
    public int maxHold = 60;
    public int bigSwingBonus = 5;
    public int stunTime = 120;
    private int animationCounter = 0;
    private int whenToSwing;
    private bool holdingPossible = true;
    private float swingChange = 0.4f;
    private bool bigSwingAnimation = false;
    private bool normalSwingAnimation = false;
    private List<GameObject> ObjectsInRange = new List<GameObject>();

    public GameObject food;
    public GameObject weapon;
    public GameObject holdSlider;

    public float swingDelay;

    // Use this for initialization
    void Start () {
        //animation = false;	
        animationCounter = swingCD;
        stunCounter = stunTime;
        whenToSwing = (int)(swingDelay*swingCD);
        Debug.Log("whenswing "+ whenToSwing);

    }
	
	// Update is called once per frame
	void Update () {
        if(stunCounter < stunTime)
        {
            weapon.transform.position = new Vector3(weapon.transform.position.x, transform.position.y - 0.1f, weapon.transform.position.z);
            stunCounter++;
        }
        //The frame user hits
        if(Input.GetButtonDown("Fire1") && stunCounter >= stunTime)
        {
            if (animationCounter >= swingCD && holdCounter == 0)
            {
                //animation = true;
                Debug.Log("Swing");
                animationCounter = 0;
                holdingPossible = true;
                normalSwingAnimation = true;
            }
        }
        //User keeps pressing
        if(Input.GetButton("Fire1") && holdingPossible)
        {
            holdCounter = Mathf.Min(holdCounter+1, maxHold);
        }
        else
        {
            //User releases
            if (Input.GetButtonUp("Fire1") && holdingPossible && stunCounter >= stunTime && holdCounter >= 0.5f*maxHold)
            {
                Debug.Log("HUGE SWING " + holdCounter);
                normalSwingAnimation = false;
                animationCounter = 0;
            }
            holdCounter = Mathf.Max(0, holdCounter-2);
            holdingPossible = false;
        }
        holdSlider.GetComponent<Slider>().value = Mathf.Min((float) holdCounter / maxHold, 1);
        if(animationCounter < swingCD)
        {

            //Debug.Log("Swing");
            //int newAngle = currentAngle + swingSpeed;
            //currentAngle = newAngle >= 360 ? 0 : newAngle;
            //weapon.transform.rotation = Quaternion.Euler(0, 0, currentAngle);
            animationCounter++;
        }
        else if(stunCounter >= stunTime)
        {
            weapon.transform.position = new Vector3(transform.position.x, transform.position.y, weapon.transform.position.z);
        }

        //animations for normal swing
        //animations for big swing
        //animations for stun animation
        if (animationCounter == whenToSwing)
        {
            if (normalSwingAnimation)
            {
                //normal swing animation
                weapon.transform.position = new Vector3(weapon.transform.position.x + swingChange, weapon.transform.position.y, weapon.transform.position.z);

                normalSwing();
            }
            else
            {
                //big swing animation
                weapon.transform.position = new Vector3(weapon.transform.position.x + swingChange*2, weapon.transform.position.y, weapon.transform.position.z);

                bigSwing();
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        col.gameObject.GetComponent<enemyLogic>().swingRange++;
        //Debug.Log("enemy detected "+ col.gameObject.GetComponent<enemyLogic>().swingRange);
        ObjectsInRange.Add(col.gameObject);
    }
    public void OnTriggerExit2D(Collider2D col)
    {
       // Debug.Log("enemy left");
        //Probably you'll have to calculate which object it is
        ObjectsInRange.Remove(col.gameObject);
    }
    
    //normal swing
    void normalSwing()
    {
        bool hit = false;
        foreach (GameObject obj in ObjectsInRange)
        {
            Debug.Log(obj.tag);
            if (obj.tag == "enemy" && obj.GetComponent<enemyLogic>().swingRange == 2)
            {

                food.GetComponent<Text>().text = (int.Parse(food.GetComponent<Text>().text) + obj.GetComponent<enemyLogic>().prize*weapon.GetComponent<weaponLogic>().foodMultiplier).ToString();
                enemyHit(obj);
                hit = true;
            }
        }
        if(!hit)
        {
            stunCounter = 0;
        }
    }
    void bigSwing()
    {
        bool hit = false;
        foreach (GameObject obj in ObjectsInRange)
        {
            Debug.Log(obj.tag);
            if (obj.tag == "enemy" && obj.GetComponent<enemyLogic>().swingRange >= 1)
            {

                food.GetComponent<Text>().text = (int.Parse(food.GetComponent<Text>().text) + obj.GetComponent<enemyLogic>().prize * weapon.GetComponent<weaponLogic>().foodMultiplier + bigSwingBonus).ToString();
                enemyHit(obj);
                hit = true;
            }
        }
        if (!hit)
        {
            stunCounter = 0;
        }
    }
    //enemy hit animation and other logic
    void enemyHit(GameObject obj)
    {
        Color tmp = obj.GetComponent<SpriteRenderer>().color;
        tmp.a = 0.5f;
        obj.GetComponent<SpriteRenderer>().color = tmp;
        obj.GetComponent<enemyLogic>().prize = 0;
    }
}
