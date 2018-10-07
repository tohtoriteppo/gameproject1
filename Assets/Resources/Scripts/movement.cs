using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour {

    public float moveSpeed;
    private float currentMoveSpeed;
    public float endSpeedBoost = 8f;

	// Use this for initialization
	void Start () {
        Camera.main.transform.position = new Vector3(transform.position.x + 1f, transform.position.y+0.2f, Camera.main.transform.position.z);
        currentMoveSpeed = moveSpeed;

    }

    // Update is called once per frame
    void Update () {
        float wolfMoveAmount = currentMoveSpeed * 0.01f;
        float cameraMoveAmount = moveSpeed * 0.01f;
        transform.position = new Vector3(transform.position.x+ wolfMoveAmount, transform.position.y, transform.position.z);
        Camera.main.transform.position = new Vector3(Camera.main.transform.position.x+ cameraMoveAmount, Camera.main.transform.position.y, Camera.main.transform.position.z);
        //<swing>().weapon.transform.position = new Vector3(GetComponent<swing>().weapon.transform.position.x, GetComponent<swing>().weapon.transform.position.y, GetComponent<swing>().weapon.transform.position.z);

    }
    public void levelEnded()
    {
        currentMoveSpeed+= endSpeedBoost;
    }
    public void startLevel()
    {
        currentMoveSpeed = moveSpeed;
    }
}
