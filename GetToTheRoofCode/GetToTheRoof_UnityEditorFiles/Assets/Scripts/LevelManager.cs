
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    
	public static LevelManager instance;

	public Image fadeScreen;
	public float fadeSpeed;
	private bool startFading;
	private float fadeCount;
	public GameObject gameOverText;

	public string victoryLevel, titleScreenLevel;

	void Awake()
	{
		instance = this;
	}


    // Start is called before the first frame update
    void Start()
    {
        startFading = false;
        fadeCount = 0f;
    }

    // Update is called once per frame
    void Update()
    {
		if(startFading)
        {
        	fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
        	if(fadeScreen.color.a == 1f)
        	{
        		//gameOverText.SetActive(true);
        	}
        	fadeCount += Time.deltaTime;
        	if(fadeCount >= 5f && PlayerController.instance.gameBeaten == false)
        	{
        		//Load a new scene here
        		SceneManager.LoadScene(titleScreenLevel);
        	} else if(fadeCount >= 5f && PlayerController.instance.gameBeaten == true)
        	{
        		SceneManager.LoadScene(victoryLevel);
        	}

        }
    }

    public void EndGame()
    {
    	startFading = true;
    }

    public void WinGame()
    {
    	startFading = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
    	if(other.tag == "Player")
    	{
    		WinGame();
    	}
    }
}
