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

    private bool isWeapon;
    private GameObject oldWeapon;
    private GameObject newWeapon;

    private string selectedItem;
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
        selectedItem = weapon;
        oldWeapon = wolf.GetComponent<swing>().weapon;
        newWeapon = Instantiate(Resources.Load("Prefabs/" + selectedItem) as GameObject, oldWeapon.transform.position, oldWeapon.transform.rotation);
        buyWindowImage.GetComponent<Image>().sprite = newWeapon.GetComponent<SpriteRenderer>().sprite;
        buyWindowPrice.GetComponent<Text>().text = newWeapon.GetComponent<weaponLogic>().price.ToString();
        if (newWeapon.GetComponent<weaponLogic>().price > GetComponent<gameController>().food)
        {
            buyButton.GetComponent<Button>().interactable = false;
        }
        else
        {
            buyButton.GetComponent<Button>().interactable = true;
        }
        isWeapon = true;


    }

    public void OpenBuyWindow2(string item)
    {
        buyWindow.SetActive(true);
        doneButton.GetComponent<Button>().interactable = false;
        selectedItem = item;
        newWeapon = Instantiate(Resources.Load("Prefabs/" + selectedItem) as GameObject);
        buyWindowImage.GetComponent<Image>().sprite = newWeapon.GetComponent<SpriteRenderer>().sprite;
        buyWindowPrice.GetComponent<Text>().text = newWeapon.GetComponent<weaponLogic>().price.ToString();
        isWeapon = false;
        if (newWeapon.GetComponent<weaponLogic>().price > GetComponent<gameController>().food)
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
        GetComponent<gameController>().updateFoodAmount(-newWeapon.GetComponent<weaponLogic>().price);
        if(isWeapon)
        {
            wolf.GetComponent<swing>().weapon = newWeapon;
            for (int i = 0; i < 5; i++)
            {
                shop.transform.GetChild(i).GetComponent<Button>().interactable = false;
                if (shop.transform.GetChild(i).name == newWeapon.name.Substring(0, 7))
                {
                    break;
                }
            }
        }
        else
        {
            shop.transform.Find(selectedItem).GetComponent<Button>().interactable = false;
            SetItem();
        }
        buyWindow.SetActive(false);
        
        //shop.transform.Find(selectedItem).GetComponent<Button>().interactable = false;
        doneButton.GetComponent<Button>().interactable = true;
        Destroy(oldWeapon);

    }

    public void CloseBuyWindow()
    {
        doneButton.GetComponent<Button>().interactable = true;
        Destroy(newWeapon);
        buyWindow.SetActive(false);
    }
    public void ResetItems()
    {
        for (int i = 5; i < 8; i++)
        {
            shop.transform.GetChild(i).GetComponent<Button>().interactable = true;
        }
        wolf.GetComponent<swing>().amulet = false;
        wolf.GetComponent<swing>().oil = false;
        wolf.GetComponent<swing>().potion = false;
    }
    public void SetItem()
    {
        if(selectedItem == "amulet")
        {
            wolf.GetComponent<swing>().amulet = true;
        }
        if(selectedItem == "oil")
        {
            wolf.GetComponent<swing>().oil = true;
        }
        if(selectedItem == "potion")
        {
            wolf.GetComponent<swing>().potion = true;
        }
    }

}
