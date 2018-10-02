using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameController : MonoBehaviour {

    public GameObject objectiveWindow;
    public GameObject targetAmount;
    public GameObject wolf;
    public GameObject shop;
    public GameObject gameOverScreen;
    public List<int> targetFoods;

    private Vector3 startPos;
    private bool levelStarted = false;
    private int currentLevel = 0;
	// Use this for initialization
	void Start () {
        targetAmount.GetComponent<Text>().text = targetFoods[currentLevel].ToString();
        wolf.GetComponent<swing>().enabled = false;
        shop.SetActive(false);
        objectiveWindow.SetActive(true);
        gameOverScreen.SetActive(false);
        startPos = wolf.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
		if(!levelStarted && Input.GetButtonDown("Fire1"))
        {
            objectiveWindow.SetActive(false);
            levelStarted = true;
            wolf.GetComponent<swing>().enabled = true;
            wolf.GetComponent<movement>().enabled = true;
            GetComponent<enemySpawner>().startLevel(currentLevel);
        }
	}
    public void levelEnded()
    {
        if(int.Parse(GetComponent<enemySpawner>().wolf.GetComponent<swing>().food.GetComponent<Text>().text)<targetFoods[currentLevel])
        {
            gameOverScreen.SetActive(true);
        }
        wolf.GetComponent<swing>().enabled = false;
        wolf.transform.position = startPos;
        wolf.GetComponent<movement>().enabled = false;
        shop.SetActive(true);
        currentLevel++;
    }
    public void shoppingDone()
    {
        shop.SetActive(false);
        objectiveWindow.SetActive(true);
        targetAmount.GetComponent<Text>().text = targetFoods[currentLevel].ToString();
        levelStarted = false;
    }
    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
