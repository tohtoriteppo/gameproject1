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
    public int increment;
    public int startTarget;
    public List<int> targetFoods;
    public int food;
    public GameObject foodText;
    public bool levelStarted = false;
    public int lives = 3;
    public GameObject pupScreen;

    private Vector3 startPos;
    private bool feedBackRead = false;
    private int currentLevel = 0;
    
	// Use this for initialization
	void Start () {
        targetAmount.GetComponent<Text>().text = startTarget.ToString();
        wolf.GetComponent<swing>().enabled = false;
        wolf.GetComponent<movement>().enabled = false;
        shop.SetActive(false);
        objectiveWindow.SetActive(true);
        gameOverScreen.SetActive(false);
        feedBackScreen.SetActive(false);
        pupScreen.SetActive(false);
        startPos = wolf.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
	}
    public void levelEnded()
    {

        wolf.transform.position = startPos;
        Camera.main.transform.position = new Vector3(wolf.transform.position.x + 1f, wolf.transform.position.y + 0.2f, Camera.main.transform.position.z);
        wolf.GetComponent<swing>().enabled = false;
        wolf.GetComponent<movement>().enabled = false;        
        feedBackScreen.SetActive(true);
        feedBackText.GetComponent<Text>().text = GetComponent<gameController>().food.ToString();
        if (food < int.Parse(targetAmount.GetComponent<Text>().text))
        {
            lives--;
        }
        else
        {
            currentLevel++;
        }
        int i;
        for (i = 0; i < lives; i++)
        {
            feedBackScreen.transform.GetChild(i).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/spr_fullPlate");
        }
        while(i<3)
        {
            feedBackScreen.transform.GetChild(i).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/spr_emptyPlate");
            i++;
        }
    }

    public void feedBackDone()
    {
        if (food < int.Parse(targetAmount.GetComponent<Text>().text))
        {
            if (lives == 0)
            {
                gameOverScreen.SetActive(true);
            }
            else
            {
                pupScreen.SetActive(true);
            }
        }
        else
        {
            feedBackScreen.SetActive(false);
            shop.SetActive(true);
            GetComponent<shopLogic>().ResetItems();
        }
        
    }

    public void shoppingDone()
    {
        shop.SetActive(false);
        objectiveWindow.SetActive(true);
        //targetAmount.GetComponent<Text>().text = targetFoods[currentLevel].ToString();
        int target = food + increment * currentLevel;
        target = target - target % 5;
        targetAmount.GetComponent<Text>().text = target.ToString();
        //levelStarted = false;
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
        wolf.GetComponent<swing>().startLevel();
        wolf.GetComponent<movement>().enabled = true;
        GetComponent<spriteSpawner>().startSprites();
        wolf.GetComponent<movement>().startLevel();
        GetComponent<enemySpawner>().startLevel(currentLevel);
    }
    public void updateFoodAmount(int amount)
    {
        food += amount;
        foodText.GetComponent<Text>().text = food.ToString();
    }
}
