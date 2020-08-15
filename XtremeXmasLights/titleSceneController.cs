using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class titleSceneController : MonoBehaviour
{
    
	public string levelToLoad;
	public Sprite[] backgroundToLoad;
	public int spritePosition;
	public float coolDownTimer;
	private float coolDownRemaining;
	private SpriteRenderer theSR;
	private Animator anim;

	//Custom background animation
	public float flickerTime;
	private float flickerTimeToSwitch;

    // Start is called before the first frame update
    void Start()
    {
        
		//For ensuring a fresh playthrough!
		PlayerPrefs.DeleteAll();
        spritePosition = 0;
        Screen.SetResolution(256, 256, true);
        coolDownRemaining = coolDownTimer;
        anim = GetComponent<Animator>();
        theSR = GetComponent<SpriteRenderer>();

        flickerTimeToSwitch = flickerTime;
    }

    // Update is called once per frame
    void Update()
    {
        
    	//Input.GetButton("Jump") This is the "Y" button"
    	//Input.GetButton("Fire1") This is the "A" button
    	//Input.GetButton("Fire2") This is the "B" button
    	//Input.GetButton("Fire3") This is the "X" button

    	//Flicker the title screen
    	
    	if(spritePosition == 0 || spritePosition == 1)
    	{
	    	
	    	if(flickerTimeToSwitch <= flickerTime/2f)
	    	{
	    		theSR.sprite = backgroundToLoad[spritePosition + 1];
	    	} else
	    	{
	    		theSR.sprite = backgroundToLoad[spritePosition];
	    	}
    	}

    	//Flicker the story screen
    	if(spritePosition == 3 || spritePosition == 4 || spritePosition == 5)
    	{
    		//Debug.Log("Flicker Time To Switch " + flickerTimeToSwitch);
    		//Debug.Log("Flicker Time " + flickerTime);
    		//Debug.Log(spritePosition);
    		//Debug.Log(flickerTimeToSwitch <= (flickerTime * 0.6666f));
    		//Debug.Log(flickerTime * 0.6666f);
    		//Debug.Log("-----------------");

    		if(flickerTimeToSwitch <= (flickerTime * 0.3333f))
    		{
    			theSR.sprite = backgroundToLoad[3];
    		} else if (flickerTimeToSwitch <= (flickerTime * 0.6666f))
    		{
    			theSR.sprite = backgroundToLoad[4];
    		} else
    		{
    			theSR.sprite = backgroundToLoad[5];
    		}
    	}


        if(Input.GetButton("Fire1") && coolDownRemaining <= 0f)
        {
        	
        	if(spritePosition == 8)
        	{
        		SceneManager.LoadScene(levelToLoad);
        	}

        	if(spritePosition == 0 || spritePosition == 4)
        	{
        		spritePosition += 2;
        	} else if(spritePosition == 3)
        	{
        		spritePosition += 3;
        	} else
        	{
        		spritePosition += 1;	
        	}
        	
        	coolDownRemaining = coolDownTimer;
        }

        if(spritePosition > 5 || spritePosition == 2)
        {
      		if(spritePosition >= 9)
      		{
      			spritePosition = 8;
      		}
      		theSR.sprite = backgroundToLoad[spritePosition];
    	}

        coolDownRemaining -= Time.deltaTime;
        flickerTimeToSwitch -= Time.deltaTime;

        if(flickerTimeToSwitch <= 0f)
        {
        	flickerTimeToSwitch = flickerTime;
        }


    }
}
