using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightPauseMenu : MonoBehaviour
{
    
	public GameObject pauseScreen;
	public GameObject infoScreen;
	public GameObject questionScreen;
	public bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        pauseScreen.SetActive(false);
        infoScreen.SetActive(false);
        questionScreen.SetActive(false);
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire2") && isPaused == false && lightLevelManager.instance.questionAsked == false && lightCameraController.instance.cameraTrip == false)
        {
        	pauseScreen.SetActive(true);
        	isPaused = true;
        	lightPlayerController.instance.canMove = false;
        	lightPlayerController.instance.theRB.velocity = new Vector2(0f,0f);
        } else if(Input.GetButtonDown("Fire2") && isPaused == true && lightLevelManager.instance.questionAsked == false && !lightCameraController.instance.cameraTrip)
        {
        	pauseScreen.SetActive(false);
        	isPaused = false;
        	lightPlayerController.instance.canMove = true;
        }

        if(Input.GetButtonDown("Fire3") && isPaused == false && lightLevelManager.instance.questionAsked == false && !lightCameraController.instance.cameraTrip)
        {
        	infoScreen.SetActive(true);
        	isPaused = true;
        	lightPlayerController.instance.canMove = false;
        	lightPlayerController.instance.theRB.velocity = new Vector2(0f,0f);
        } else if(Input.GetButtonDown("Fire3") && isPaused == true && lightLevelManager.instance.questionAsked == false && !lightCameraController.instance.cameraTrip)
        {
        	infoScreen.SetActive(false);
        	isPaused = false;
        	lightPlayerController.instance.canMove = true;
        }


    }
}
