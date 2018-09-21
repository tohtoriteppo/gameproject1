using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour {

    public int spawnCD;
    public int enemyAmount;

    private int spawnCounter;

    public GameObject enemyPrefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
        if(spawnCounter > spawnCD)
        {
            //random
        }
	}
}
