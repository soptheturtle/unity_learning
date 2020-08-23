using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    
	public static playerController instance;
	public Rigidbody2D theRB;
	public float currentMoveSpeed;

	public GameObject quitMenu;
	public bool quittingGame;
	public float quitTime;
	private float currentQuitTime;
	

	public int restPoints, karmaPoints;

	public bool canMove;

	void Awake()
	{
		instance = this;
	}


    // Start is called before the first frame update
    void Start()
    {
        theRB = GetComponent<Rigidbody2D>();
        canMove = true;
        quittingGame = false;
        currentQuitTime = quitTime;
    }

    // Update is called once per frame
    void Update()
    {
        
    	//Handle quitting menu and loop
    	if(Input.GetButtonDown("Fire2") && !quittingGame)
    	{
    
	   		canMove = false;
    		quitMenu.SetActive(true);
    		quittingGame = true;
    	}

    	//To ensure we don't activate and reactive the menu in the same frame
    	//Ideally I should do this in an entirely separate script. But I didn't because I ran out of time
    	if(quittingGame)
    	{
    		currentQuitTime -= Time.deltaTime;
    	}

    	if(quittingGame && currentQuitTime <= 0f)
    	{
    		if(Input.GetButtonDown("Fire1"))
    		{
    			Application.Quit();
    		} else if(Input.GetButtonDown("Fire2"))
    		{
    			quitMenu.SetActive(false);
    			canMove= true;
    			quittingGame = false;
    			currentQuitTime = quitTime;
    		}
    	}




    	//Standard Player Movement
    	if(canMove)
    	{
	        theRB.velocity = new Vector2(currentMoveSpeed * Input.GetAxis("Horizontal") , currentMoveSpeed * Input.GetAxis("Vertical"));
			if(Mathf.Abs(theRB.velocity.x) > 0.2f || Mathf.Abs(theRB.velocity.y) > 0.2f)
        	{
        		partyAudioController.instance.PlaySFXWalking();
        	} else
        	{     		
        		partyAudioController.instance.StopSFXWalking();
        	}


        } else
        {
    		theRB.velocity = new Vector2(0f,0f);
    	}

    	if(!canMove)
    	{
    		partyAudioController.instance.StopSFXWalking();
    	}

    	//Ensure we don't exceed or go below zero on the number of rest and karma points
    	CheckTotalPointsGoodness();

    }


    private void CheckTotalPointsGoodness()
    {
    	if(restPoints > 50)
    	{
    		restPoints = 50;
    	}
    	if(karmaPoints > 50)
    	{
    		karmaPoints = 50;
    	}
    	if(restPoints < 0)
    	{
    		restPoints = 0;
    	}
    	if(karmaPoints < 0)
    	{
    		karmaPoints = 0;
    	}



    }
}
