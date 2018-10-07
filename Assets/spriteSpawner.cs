using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spriteSpawner : MonoBehaviour
{

    public float chanceToSpawn;
    public int maxSpawnCD;
    public int spawnCD;
    public int spriteAmount;
    public GameObject wolf;
    public List<string> sprites;
    public GameObject[] spriteObjects;
    public bool levelStarted = false;

    private int spawnCounter;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<gameController>().levelStarted)
        {

            if (spawnCounter > spawnCD)
            {
                int toSpawn = Random.Range(0, 100);
                if (toSpawn <= chanceToSpawn || spawnCounter > maxSpawnCD)
                {
                    int whichSprite = Random.Range(0, spriteAmount);
                    Vector3 spawnPos = new Vector3(wolf.transform.position.x + 3.5f, wolf.transform.position.y, wolf.transform.position.z);
                    Instantiate(Resources.Load("Prefabs/" + sprites[whichSprite]) as GameObject, spawnPos, wolf.transform.rotation);
                    spawnCounter = 0;
                    //enemyCounter++;
                }

            }
            spawnCounter++;
        }
    }
    public void destroySprites()
    {
        spriteObjects = GameObject.FindGameObjectsWithTag("sprite");
        for (int i = 0; i < spriteObjects.Length; i++)
        {
            Destroy(spriteObjects[i]);
        }
    }
    public void startSprites()
    {
        for(int i = 0; i < 7; i++)
        {
            int whichSprite = Random.Range(0, spriteAmount);
            int randomDistance = Random.Range(0, 10);
            Vector3 spawnPos = new Vector3(wolf.transform.position.x + i*0.4f+ randomDistance/10, wolf.transform.position.y, wolf.transform.position.z);
            Instantiate(Resources.Load("Prefabs/" + sprites[whichSprite]) as GameObject, spawnPos, wolf.transform.rotation);

        }
        

    }
}