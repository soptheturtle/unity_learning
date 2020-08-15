using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class lightLevelManager : MonoBehaviour
{
    

	public static lightLevelManager instance;
	//Levels to Load
	public string levelToLoad;
	public GameObject thePlayer;
	public float distanceToLoad;
	public GameObject questionCanvas;

	public bool questionAsked;
	private bool dontLoad;

	void Awake()
	{
		instance = this;
	}
    // Start is called before the first frame update
    void Start()
    {
    	questionAsked = false;
    	dontLoad = false;
    }

    // Update is called once per frame
    void Update()
    {
	if(questionAsked == true && Input.GetButtonDown("Fire1"))
        {
        	
			if(lightCameraController.instance.cameraTrip == false)
			{
				lightAudioManager.instance.StartLevelEndMusic();
			}
        	lightCameraController.instance.cameraTrip = true;
        	questionCanvas.SetActive(false);

        } else if(questionAsked == true && Input.GetButtonDown("Fire2"))
        {
        	questionCanvas.SetActive(false);
        	StartCoroutine(EnablePause());
        	if(lightPlayerController.instance.pieceLimit > 0)
        	{
        	lightPlayerController.instance.canPlace = true;
        	}
        	lightPlayerController.instance.canMove = true;
        	dontLoad = true;
        }  
      
        if(Vector3.Distance(gameObject.transform.position, thePlayer.transform.position) < distanceToLoad && Input.GetButtonDown("Fire1") && questionAsked == false && !dontLoad)
        {
        	questionCanvas.SetActive(true);
        	questionAsked = true;
        	lightPlayerController.instance.canPlace = false;
        	lightPlayerController.instance.canMove = false;
        	
        	
        }

        dontLoad = false;
        



    }

    public void LoadTheEnd()
    {
    StartCoroutine(EndLevelCo());
    }

    public IEnumerator EndLevelCo()
    {
    	yield return new WaitForSeconds(3.0f);
    	SceneManager.LoadScene(levelToLoad);
    	PlayerPrefs.SetInt("PlayerPoints",lightPlayerController.instance.playerPoints);
    	PlayerPrefs.SetInt("PiecesRemaining",lightPlayerController.instance.pieceLimit);
    }

    public IEnumerator EnablePause()
    {
    	yield return new WaitForSeconds(0.5f);
    	questionAsked = false;
    }


}
