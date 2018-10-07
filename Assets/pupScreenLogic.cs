﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pupScreenLogic : MonoBehaviour {

    public gameController controller;
    // Use this for initialization
    void Start()
    {
        controller = Camera.main.GetComponent<gameController>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            gameObject.SetActive(false);
            controller.feedBackScreen.SetActive(false);
            controller.shop.SetActive(true);
            controller.GetComponent<shopLogic>().ResetItems();
        }
    }
}
