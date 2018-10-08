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
    private int animationCounter = 0;
    private int whenToSwing;
    private bool holdingPossible = true;
    private float swingChange = 0.35f;
    private bool bigSwingAnimation = false;
    private bool normalSwingAnimation = false;
    private List<GameObject> ObjectsInRange = new List<GameObject>();
    private int maxHold = 60;
    private int stunTime = 120;
    private float colliderSize1 = 1;
    private float colliderSize2 = 1.25f;

    public int originalMaxHold = 60;
    public int swingCD = 60;
    public int bigSwingBonus = 5;
    public int originalStunTime = 120;
    public Animator animator;
    public List<RuntimeAnimatorController> controllers;
    public GameObject weapon;
    public GameObject holdSlider;
    public bool amulet = false;
    public bool oil = false;
    public bool potion = false;
    public bool originalHoldMechanic = false;

    public float swingDelay;

    // Use this for initialization
    void Start () {
        //animation = false;	
        animationCounter = swingCD;
        stunCounter = stunTime;
        whenToSwing = (int)(swingDelay*swingCD);
        setHitBoxes();

    }
	
	// Update is called once per frame
	void Update () {

        //stun is affecting
        if(stunCounter < stunTime)
        {
            weapon.transform.position = new Vector3(transform.position.x, transform.position.y - 0.1f, weapon.transform.position.z);
            stunCounter++;
        }
        //The frame user hits
        if (originalHoldMechanic)
        {
            if (Input.GetButtonDown("Fire1") && stunCounter >= stunTime)
            {
                if (animationCounter >= swingCD && holdCounter == 0)
                {
                    //animation = true;
                    animationCounter = 0;
                    holdingPossible = true;
                    normalSwingAnimation = true;
                    animator.SetBool("swing", true);
                    whenToSwing = (int)(swingDelay * swingCD);
                }
            }
            //User keeps pressing
            if (Input.GetButton("Fire1") && holdingPossible)
            {
                holdCounter = Mathf.Min(holdCounter + 1, maxHold);
            }
            else
            {
                //User releases
                if (Input.GetButtonUp("Fire1") && holdingPossible && stunCounter >= stunTime && holdCounter >= 0.5f * maxHold)
                {
                    normalSwingAnimation = false;
                    animator.SetBool("big swing", true);
                    animator.SetBool("swing", false);
                    whenToSwing = (int)(swingCD*10);
                    animationCounter = 0;
                }
                holdCounter = Mathf.Max(0, holdCounter - 2);
                holdingPossible = false;
            }
            holdSlider.GetComponent<Slider>().value = Mathf.Min((float)holdCounter / maxHold, 1);
            if (animationCounter < swingCD)
            {

                //int newAngle = currentAngle + swingSpeed;
                //currentAngle = newAngle >= 360 ? 0 : newAngle;
                //weapon.transform.rotation = Quaternion.Euler(0, 0, currentAngle);
                animationCounter++;
            }
            else
            {
                animator.SetBool("swing", false);
                animator.SetBool("big swing", false);
            }
            if (stunCounter >= stunTime)
            {
                weapon.transform.position = new Vector3(transform.position.x, transform.position.y, weapon.transform.position.z);
                animator.SetBool("miss", false);
            }
            //might need to change place
            

            //animations for normal swing
            //animations for big swing
            //animations for stun animation
            if (animationCounter == whenToSwing)
            {
                if (normalSwingAnimation)
                {
                    //normal swing animation
                    weapon.transform.position = new Vector3(transform.position.x + swingChange, weapon.transform.position.y, weapon.transform.position.z);

                    normalSwing();
                }
                else
                {
                    //big swing animation
                    weapon.transform.position = new Vector3(transform.position.x + swingChange * 2, weapon.transform.position.y, weapon.transform.position.z);

                    bigSwing();
                }
            }
        }
        else
        {
            //User hits, is not stunned and swingCD has passed.
            if (Input.GetButtonDown("Fire1") && stunCounter >= stunTime && animationCounter >= swingCD)
            {
                if (holdCounter == 0)
                {
                    Debug.Log("PRESSED");
                    animationCounter = 0;
                    holdingPossible = true;
                    animation = false;
                }
            }
            //User keeps pressing
            if (Input.GetButton("Fire1") && holdingPossible)
            {
                Debug.Log("HOLD");
                holdCounter = Mathf.Min(holdCounter + 1, maxHold);
            }
            //When not pressing, the slider will lose value
            else
            {
                holdCounter = Mathf.Max(0, holdCounter - 2);
                //holdingPossible = false;
            }
            //User releases or holding is at max value
            //Debug.Log("values "+holdCounter + " " + maxHold);
            if ((Input.GetButtonUp("Fire1") && holdingPossible) || holdCounter == maxHold)
            {
                if(holdCounter >= 0.5f * maxHold)
                {
                    normalSwingAnimation = false;
                    animator.SetBool("big swing", true);
                    animator.SetBool("swing", false);
                    animationCounter = 0;
                    animation = true;
                }
                else
                {
                    normalSwingAnimation = true;
                    animator.SetBool("big swing", false);
                    animator.SetBool("swing", true);
                    animationCounter = 0;
                    animation = true;
                }
                holdingPossible = false;
            } 
            holdSlider.GetComponent<Slider>().value = Mathf.Min((float)holdCounter / maxHold, 1);
            //animationCounter goes on until swingCD has passed
            if (animationCounter < swingCD && animation)
            {
                animationCounter++;
            }
            //If not then if stun has passed, set animation to false
            else if (stunCounter >= stunTime)
            {
                weapon.transform.position = new Vector3(transform.position.x, transform.position.y, weapon.transform.position.z);
                animator.SetBool("miss", false);
                animation = false;
            }
            //might need to change place
            else
            {
                animator.SetBool("swing", false);
                animator.SetBool("big swing", false);
                animation = false;
            }
            if (animationCounter == whenToSwing)
            {
                if (normalSwingAnimation)
                {
                    //normal swing animation
                    weapon.transform.position = new Vector3(transform.position.x + swingChange, weapon.transform.position.y, weapon.transform.position.z);

                    normalSwing();
                }
                else
                {
                    //big swing animation
                    weapon.transform.position = new Vector3(transform.position.x + swingChange * 2, weapon.transform.position.y, weapon.transform.position.z);

                    bigSwing();
                }
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        
        //Debug.Log("enemy detected "+ col.gameObject.GetComponent<enemyLogic>().swingRange);
        
        if(col.gameObject.tag == "enemy")
        {
            col.gameObject.GetComponent<enemyLogic>().swingRange++;
            if (!ObjectsInRange.Contains(col.gameObject))
            {
                ObjectsInRange.Add(col.gameObject);
            }
        }
    }
    public void OnTriggerExit2D(Collider2D col)
    {
        // Debug.Log("enemy left");
        //Probably you'll have to calculate which object it is
        if (ObjectsInRange.Contains(col.gameObject))
        {
            ObjectsInRange.Remove(col.gameObject);
        }
    }

    //normal swing
    void normalSwing()
    {
        bool hit = false;
        foreach (GameObject obj in ObjectsInRange)
        {
            if (obj.tag == "enemy" && obj.GetComponent<enemyLogic>().swingRange == 2)
            {
                int bonus = amulet == true ? 1 : 0;
                int amount = (obj.GetComponent<enemyLogic>().prize + weapon.GetComponent<weaponLogic>().weaponBonus + bonus);
                spawnCrumbs(amount, obj);
                //Camera.main.GetComponent<gameController>().updateFoodAmount(amount);
                enemyHit(obj);
                hit = true;
            }
        }
        if(!hit)
        {
            stunCounter = 0;
            animator.SetBool("miss", true);
            //holdingPossible = false;
        }
    }
    void bigSwing()
    {
        bool hit = false;
        foreach (GameObject obj in ObjectsInRange)
        {
            if (obj.tag == "enemy" && obj.GetComponent<enemyLogic>().swingRange >= 1)
            {
                int bonus = amulet == true ? 1 : 0;
                int amount = (obj.GetComponent<enemyLogic>().prize + weapon.GetComponent<weaponLogic>().weaponBonus + bigSwingBonus + bonus);
                spawnCrumbs(amount, obj);
                //Camera.main.GetComponent<gameController>().updateFoodAmount(amount);
                enemyHit(obj);
                obj.GetComponent<Animator>().SetBool("hit", true);
                hit = true;
            }
        }
        if (!hit)
        {
            stunCounter = 0;
            animator.SetBool("miss", true);
            //holdingPossible = false;
        }
    }
    //enemy hit animation and other logic
    void enemyHit(GameObject obj)
    {
        //Color tmp = obj.GetComponent<SpriteRenderer>().color;
        //tmp.a = 0.5f;
        //obj.GetComponent<SpriteRenderer>().color = tmp;
        obj.GetComponent<enemyLogic>().prize = 0;
        obj.GetComponent<enemyLogic>().hit = true;

    }

    void spawnCrumbs(int amount, GameObject obj)
    {
        //Debug.Log("SWPANANA");
        while(amount >= 5)
        {
            GameObject carrot = Instantiate(Resources.Load("Prefabs/carrot") as GameObject, obj.transform.position, obj.transform.rotation);
            carrot.GetComponent<BoxCollider2D>().enabled = false;
            amount -= 5;
        }
        while(amount > 0)
        {
            GameObject crumb = Instantiate(Resources.Load("Prefabs/crumb") as GameObject, obj.transform.position, obj.transform.rotation);
            crumb.GetComponent<BoxCollider2D>().enabled = false;
            amount -= 1;
        }
        

    }

    public void startLevel()
    {
        stunTime = oil == true ? originalStunTime/2 : originalStunTime;
        maxHold = potion == true ? originalMaxHold / 2 : originalMaxHold;
    }
    public void walkAnimation()
    {
        animator.SetBool("big swing", false);
        animator.SetBool("swing", false);
        //animator.SetBool("miss", false);
    }
    
    public void setHitBoxes()
    {
        BoxCollider2D[] colliders = GetComponents<BoxCollider2D>();
        float multiplier = weapon.GetComponent<weaponLogic>().hitBoxMultiplier;
        float change1 =  (multiplier * colliderSize1 - colliders[0].size.x)/2;
        float change2 = (multiplier * colliderSize1 +0.25f - colliders[1].size.x)/2;
        colliders[0].size = new Vector2(multiplier * colliderSize1, colliders[0].size.y);
        colliders[1].size = new Vector2(multiplier * colliderSize1 + 0.25f, colliders[1].size.y);
        colliders[0].offset = new Vector2(colliders[0].offset.x + change1, colliders[0].offset.y);
        colliders[1].offset = new Vector2(colliders[1].offset.x + change2, colliders[1].offset.y);
    }
}
