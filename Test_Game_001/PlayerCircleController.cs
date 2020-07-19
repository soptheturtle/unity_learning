using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerCircleController : MonoBehaviour
{
	
	public static PlayerCircleController instance;


	public float moveSpeed;
	private float currentMoveSpeed;
	public Rigidbody2D theRB;
	private bool isRunning;
	
	
	//Sprite items
	public Sprite upSprite,leftSprite,downSprite,rightSprite;

	

	private Animator anim;
	private SpriteRenderer theSR;


	public bool stopInput;
	public float runMultiplier;
	private float runtoApply;

 	//Sword Items
	public GameObject playerSword;
	public Transform swordStartPoint;
	public float swordDistance;
	public Transform swordEndPoint;
	public float swordSpeed;
	private bool swordActive;
	private int swordDirection; //0 = up, 1 = left, 2 = down, 3 = right

	private void Awake()
	{
		instance = this;
		runtoApply = 1f;
	}

    // Start is called before the first frame update
    void Start()
    {
        
        anim = GetComponent<Animator>();
        theSR = GetComponent<SpriteRenderer>();
        currentMoveSpeed = moveSpeed;
        swordActive = false;
        swordDirection = 0;
        isRunning = false;
        playerSword.transform.position = gameObject.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
    	
    	//******************
    	//Player Controller
    	//******************

    	//Check if player is running
    	if(Input.GetButton("Jump"))
    	{
    		if(isRunning == false)
    		{
    			currentMoveSpeed *= runMultiplier;
    			isRunning = true;
			}
				
			
    	} else 
    	{
    		isRunning = false;
    		currentMoveSpeed = moveSpeed;
    	}
    	
       
    	
       	theRB.velocity = new Vector2(currentMoveSpeed * Input.GetAxis("Horizontal") , currentMoveSpeed * Input.GetAxis("Vertical") * runtoApply);


        //**********************************************
        //Determine what way the sprite should be facing
        //Also determine the sword direction
        //**********************************************
        if(Mathf.Abs(theRB.velocity.x) > 0.05 || Mathf.Abs(theRB.velocity.y) > 0.05)
        {
        	
        	//Handle it going up
        	if(theRB.velocity.y >= 0)
        	{
        		if(Mathf.Abs(theRB.velocity.y) >= Mathf.Abs(theRB.velocity.x))
        		{
        			swordDirection = 0;
        			theSR.sprite = upSprite;
        		} else if(theRB.velocity.x < 0)
        		{
        			swordDirection = 1;
        			theSR.sprite = leftSprite;
        		} else if(theRB.velocity.x > 0)
        		{
        			swordDirection = 3;
        			theSR.sprite = rightSprite;
        		}
        	} 

        	//Handle it going down
        	if(theRB.velocity.y < 0)
        	{
        		if(Mathf.Abs(theRB.velocity.y) >= Mathf.Abs(theRB.velocity.x))
        		{
        			swordDirection = 2;
        			theSR.sprite = downSprite;
        		} else if(theRB.velocity.x < 0)
        		{
        			swordDirection = 1;
        			theSR.sprite = leftSprite;
        		} else if(theRB.velocity.x > 0)
        		{
        			swordDirection = 3;
        			theSR.sprite = rightSprite;
        		}
        	}
		}

		//*********************
		//Sword Control
		//*********************
		if(Input.GetButtonDown("Fire1") && swordActive == false)
		{
			
			swordActive = true;
			playerSword.SetActive(true);
			//swordStartPoint.position = new Vector3(gameObject.transform.position.x +0.0f, gameObject.transform.position.y + 0.0f, gameObject.transform.position.z);

			switch(swordDirection)
			{
				case 0:
				swordEndPoint.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + swordDistance, gameObject.transform.position.z);
				playerSword.transform.eulerAngles = new Vector3(playerSword.transform.eulerAngles.x, playerSword.transform.eulerAngles.y, 90);
				break;
				case 1:
				swordEndPoint.position = new Vector3(gameObject.transform.position.x - swordDistance, gameObject.transform.position.y, gameObject.transform.position.z);
				playerSword.transform.eulerAngles = new Vector3(playerSword.transform.eulerAngles.x, playerSword.transform.eulerAngles.y, 180);
				break;
				case 2:
				swordEndPoint.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y -swordDistance, gameObject.transform.position.z);
				playerSword.transform.eulerAngles = new Vector3(playerSword.transform.eulerAngles.x, playerSword.transform.eulerAngles.y, 270);
				break;
				case 3:
				swordEndPoint.position = new Vector3(gameObject.transform.position.x + swordDistance, gameObject.transform.position.y, gameObject.transform.position.z);
				playerSword.transform.eulerAngles = new Vector3(playerSword.transform.eulerAngles.x, playerSword.transform.eulerAngles.y, 0);
				break;
			}

		}

		 if(swordActive)
		 {
		 	playerSword.transform.position = Vector3.MoveTowards(playerSword.transform.position, swordEndPoint.position, swordSpeed * Time.deltaTime);
		 	if(Vector3.Distance(playerSword.transform.position,swordEndPoint.position) < 0.05f)
		 	{
		 		swordActive = false;
		 		playerSword.SetActive(false);
		 		playerSword.transform.position = gameObject.transform.position;
		 	}

		 }

        
        
	}

   
}
