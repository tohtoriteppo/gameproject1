using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour {

    public int spawnCD;
    public int chanceToSpawn;
    public int maxSpawnCD;
    public List<string> level1enemies;
    public GameObject wolf;
    public int enemyAmount;

    private int enemyCounter = 0;
    private int spawnCounter = 0;
    private bool levelStarted = false;
    private GameObject[] enemies;
    private Vector2 resolution;
    //public List<string> level2enemies;
    //public List<string> level3enemies;
    // Use this for initialization
    void Start () {
        resolution = new Vector2(1920, 1080);
	}
	
	// Update is called once per frame
	void Update () {
		
        //if enough time has passed, spawn an enemy
        if(levelStarted)
        {
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
            if (enemyCounter > enemyAmount)
            {
                endLevel();
            }
        }

    }

    public void startLevel(int levelNumber)
    {
        levelStarted = true;
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
        levelStarted = false;
    }
}
