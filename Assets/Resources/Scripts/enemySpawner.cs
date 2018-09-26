using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour {

    public int spawnCD;
    public int chanceToSpawn;
    public int maxSpawnCD;

    public int enemyAmount;
    private int spawnCounter = 0;
    private Vector2 resolution;
    public List<string> level1enemies;
    public GameObject knight;
    //public List<string> level2enemies;
    //public List<string> level3enemies;
    // Use this for initialization
    void Start () {
        resolution = new Vector2(1920, 1080);
	}
	
	// Update is called once per frame
	void Update () {
		
        //if enough time has passed, spawn an enemy
        if(spawnCounter > spawnCD)
        {
            int toSpawn= Random.Range(0, 100);
            if(toSpawn <= chanceToSpawn || spawnCounter > maxSpawnCD)
            {
                int whichEnemy = Random.Range(0, level1enemies.Count);
                
                Vector3 spawnPos = new Vector3(knight.transform.position.x +3, knight.transform.position.y, knight.transform.position.z);
                Instantiate(Resources.Load("Prefabs/" + level1enemies[whichEnemy]) as GameObject, spawnPos, knight.transform.rotation);
                spawnCounter = 0;
            }
            
        }
        spawnCounter++;


    }
}
