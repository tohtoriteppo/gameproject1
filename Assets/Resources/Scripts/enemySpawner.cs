using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemySpawner : MonoBehaviour {

    public int spawnCD;
    public int chanceToSpawn;
    public int maxSpawnCD;
    public List<string> level1enemies;
    public GameObject wolf;
    public int enemyAmount;
    public int levelTime = 50;
    public float howFarToEndLevel = 2.2f;
    public GameObject timeSlider;

    private int timeCounter = 0;
    private int timeLimit = 0;
    private int enemyCounter = 0;
    private int spawnCounter = 0;
    //private bool levelStarted = false;
    private GameObject[] enemies;
    private Vector2 resolution;
    //public List<string> level2enemies;
    //public List<string> level3enemies;
    // Use this for initialization
    void Start () {
        resolution = new Vector2(1920, 1080);
        timeLimit = levelTime * 60;
	}
	
	// Update is called once per frame
	void Update () {
		
        //if enough time has passed, spawn an enemy
        if(GetComponent<gameController>().levelStarted)
        {
            timeCounter++;
            timeSlider.GetComponent<Slider>().value = Mathf.Min((float)(timeLimit-timeCounter) / timeLimit, 1);
            if (timeCounter > timeLimit)
            {
                wolf.GetComponent<movement>().levelEnded();
                GetComponent<gameController>().levelStarted = false;
                timeCounter = 0;
            }

            if (spawnCounter > spawnCD)
            {
                int toSpawn = Random.Range(0, 100);
                if (toSpawn <= chanceToSpawn || spawnCounter > maxSpawnCD)
                {
                    int whichEnemy = Random.Range(0, level1enemies.Count);
                    Vector3 spawnPos = new Vector3(wolf.transform.position.x + 3, wolf.transform.position.y, wolf.transform.position.z);
                    Instantiate(Resources.Load("Prefabs/" + level1enemies[whichEnemy]) as GameObject, spawnPos, wolf.transform.rotation);
                    spawnCounter = 0;
                    enemyCounter++;
                }

            }
            spawnCounter++;

            //level ends
            /*
            if (enemyCounter > enemyAmount)
            {
                endLevel();
            }
            */
        }
        else if(wolf.transform.position.x > Camera.main.transform.position.x+ howFarToEndLevel)
        {
            endLevel();

        }

    }

    public void startLevel(int levelNumber)
    {
        //levelStarted = true;
    }
    private void endLevel()
    {
        enemyCounter = 0;
        enemies = GameObject.FindGameObjectsWithTag("enemy");
        for(int i = 0; i < enemies.Length; i++)
        {
            Destroy(enemies[i]);
        }
        GetComponent<gameController>().levelEnded();
        GetComponent<spriteSpawner>().destroySprites();

        
    }
}
