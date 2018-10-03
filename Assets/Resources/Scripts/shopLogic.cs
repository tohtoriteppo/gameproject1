using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shopLogic : MonoBehaviour {

    public GameObject shop;
    public GameObject buyWindow;
    public GameObject wolf;
    public GameObject doneButton;
    public GameObject buyWindowImage;
    public GameObject buyWindowPrice;
    public GameObject buyButton;

    private GameObject oldWeapon;
    private GameObject newWeapon;

    private string selectedWeapon;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
   
    public void OpenBuyWindow(string weapon)
    {
        buyWindow.SetActive(true);
        doneButton.GetComponent<Button>().interactable = false;
        selectedWeapon = weapon;
        oldWeapon = wolf.GetComponent<swing>().weapon;
        newWeapon = Instantiate(Resources.Load("Prefabs/" + selectedWeapon) as GameObject, oldWeapon.transform.position, oldWeapon.transform.rotation);
        buyWindowImage.GetComponent<Image>().sprite = newWeapon.GetComponent<SpriteRenderer>().sprite;
        buyWindowPrice.GetComponent<Text>().text = newWeapon.GetComponent<weaponLogic>().price.ToString();
        GameObject foodText = GetComponent<enemySpawner>().wolf.GetComponent<swing>().food;
        if (newWeapon.GetComponent<weaponLogic>().price > int.Parse(foodText.GetComponent<Text>().text))
        {
            buyButton.GetComponent<Button>().interactable = false;
        }
        else
        {
            buyButton.GetComponent<Button>().interactable = true;
        }


    }

    public void BuySelectedItem()
    {
        
        GameObject foodText = GetComponent<enemySpawner>().wolf.GetComponent<swing>().food;
        foodText.GetComponent<Text>().text = (int.Parse(foodText.GetComponent<Text>().text)- newWeapon.GetComponent<weaponLogic>().price).ToString();
        wolf.GetComponent<swing>().weapon = newWeapon;
        buyWindow.SetActive(false);
        doneButton.GetComponent<Button>().interactable = true;
        Destroy(oldWeapon);

    }

    public void CloseBuyWindow()
    {
        doneButton.GetComponent<Button>().interactable = true;
        Destroy(newWeapon);
        buyWindow.SetActive(false);
    }

}
