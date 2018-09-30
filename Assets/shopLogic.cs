using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shopLogic : MonoBehaviour {

    public GameObject shop;
    public GameObject buyMenu;
    public GameObject wolf;

    private string selectedWeapon;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
   
    public void OpenBuyWindow(string weapon)
    {
        buyMenu.SetActive(true);
        selectedWeapon = weapon;
    }

    public void BuySelectedItem()
    {
        GameObject oldWeapon = wolf.GetComponent<swing>().weapon;
        GameObject newWeapon = Instantiate(Resources.Load("Prefabs/" + selectedWeapon) as GameObject, oldWeapon.transform.position, oldWeapon.transform.rotation);
        if(newWeapon.GetComponent<weaponLogic>().price <= int.Parse(GetComponent<enemySpawner>().knight.GetComponent<swing>().food.GetComponent<Text>().text))
        {

        }
        {

        }
        wolf.GetComponent<swing>().weapon = newWeapon;
        Destroy(oldWeapon);


    }

    public void CloseBuyWindow()
    {
        buyMenu.SetActive(false);
    }

}
