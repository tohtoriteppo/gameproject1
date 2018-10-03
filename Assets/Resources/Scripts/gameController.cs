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
    public GameObject feedBackScreen;
    public GameObject feedBackText;
    public List<int> targetFoods;

    private Vector3 startPos;
    private bool levelStarted = false;
    private bool feedBackRead = false;
    private int currentLevel = 0;
	// Use this for initialization
	void Start () {
        targetAmount.GetComponent<Text>().text = targetFoods[currentLevel].ToString();
        wolf.GetComponent<swing>().enabled = false;
        wolf.GetComponent<movement>().enabled = false;
        shop.SetActive(false);
        objectiveWindow.SetActive(true);
        gameOverScreen.SetActive(false);
        feedBackScreen.SetActive(false);
        startPos = wolf.transform.position;
    }
	
	// Update is called once per frame
	void Update () {

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
        currentLevel++;
        feedBackScreen.SetActive(true);
        feedBackText.GetComponent<Text>().text = GetComponent<enemySpawner>().wolf.GetComponent<swing>().food.GetComponent<Text>().text.ToString();
    }

    public void feedBackDone()
    {

        shop.SetActive(true);
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
    public void startLevel()
    {
        objectiveWindow.SetActive(false);
        levelStarted = true;
        wolf.GetComponent<swing>().enabled = true;
        wolf.GetComponent<movement>().enabled = true;
        GetComponent<enemySpawner>().startLevel(currentLevel);
    }
}
