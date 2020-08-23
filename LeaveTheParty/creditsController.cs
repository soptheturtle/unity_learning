using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class creditsController : MonoBehaviour
{
    

	public GameObject resultDisplay;
	public SpriteRenderer resultDisplaySR;
	public Sprite[] resultSprite;
	public int i;

	public GameObject goodJob;
	public GameObject badJob;
	public GameObject terribleJob;

	public string levelToLoad;

	public float waitTimeToHit;

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetInt("totalPoints") == 100)
        {
        	i = 0; //A+
        } else if(PlayerPrefs.GetInt("totalPoints") >= 80)
        {
        	i = 1; //A
        } else if(PlayerPrefs.GetInt("totalPoints") >= 60)
        {
        	i = 2; //B
        } else if (PlayerPrefs.GetInt("totalPoints") >= 40)
        {
        	i = 3; //C
        } else if (PlayerPrefs.GetInt("totalPoints") >= 20)
        {
        	i = 4; //D
        } else if (PlayerPrefs.GetInt("totalPoints") > 5)
        {
        	i = 5; //F
        } else
        {
        	i = 6; //F-
        }

        resultDisplaySR.sprite = resultSprite[i];
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerPrefs.GetInt("totalPoints") >= 60)
        {
        	goodJob.SetActive(true);
        } else if (PlayerPrefs.GetInt("totalPoints") >= 20)
        {
        	badJob.SetActive(true);
        } else{
        	terribleJob.SetActive(true);
        }

        if(waitTimeToHit >= 0)
        {
        	waitTimeToHit -= Time.deltaTime;
    	}
        if(waitTimeToHit <= 0)
        {
        	if(Input.GetButtonDown("Fire1"))
        	{
        		SceneManager.LoadScene(levelToLoad);
        	}
        }
    }
}
