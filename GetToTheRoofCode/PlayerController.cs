using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
	public static PlayerController instance;
	private Rigidbody2D theRB;
	private SpriteRenderer theSR;
	private Animator anim;
	public float moveSpeed;
	private float numCycles;
	public GameObject presserPuzzle;
	public GameObject chestPuzzle,chestPuzzleChest,chestPuzzleKeyring;
	public float keyToUse;
	public GameObject theClocks;

	public Transform bottomPoint, topPoint, lowerLimit, upperLimit;

	private bool hasMoved;
	public float untilMoveAgain;
	private float untilMoveAgainCountDown;
	public bool beginChestPuzzle;
	public bool canSelectKeys;
	public bool selectingKeys;
	public int selectedKey;

	public float spawnEnemy;
	public GameObject tentacleEnemy;

	public GameObject contextClueA;

	public bool hasBugSpray;
	public GameObject bugSprayLeft, bugSprayRight;
	private bool isSpraying;
	public bool gameBeaten;

	//Needed to set an instance
	void Awake()
	{
		instance = this;

	}

    // Start is called before the first frame update
    void Start()
    {
        theRB = GetComponent<Rigidbody2D>();
        theSR = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        numCycles = 0;
        beginChestPuzzle = false;
        selectedKey = 0;
        hasMoved = false;
		canSelectKeys = false;
		selectingKeys = false;
		selectedKey = -1;
		hasBugSpray = false;
		isSpraying = false;
		gameBeaten = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        
    	//Can we move back and forth
    	if(!selectingKeys)
    	{
        	theRB.velocity = new Vector2(moveSpeed * Input.GetAxis("Horizontal"), theRB.velocity.y);
    	}
        
        //Set the player movement animation and flip the sprite based on direction
        anim.SetFloat("PlayerSpeed",Mathf.Abs(theRB.velocity.x));
        if(theRB.velocity.x <0)
        {
        	theSR.flipX = true;
        } else if(theRB.velocity.x > 0)
        {
        	theSR.flipX = false;
        }

        //Countdown timer for the looping so it doesn't freak out
        if(hasMoved)
        {
        	untilMoveAgainCountDown -= Time.deltaTime;
        	if(untilMoveAgainCountDown < 0f)
        	{
        		hasMoved = false;
        		untilMoveAgainCountDown = untilMoveAgain;
        	}
        }

        //Check to make sure you haven't hit either edge of the stairs
        if(transform.position.y > upperLimit.position.y && !gameBeaten)
        {
        	transform.position = new Vector3(bottomPoint.position.x, bottomPoint.position.y, transform.position.z);
        }
        if(transform.position.y < lowerLimit.position.y)
        {
        	transform.position = new Vector3(topPoint.position.x, topPoint.position.y, transform.position.z);
        }

        //Key selecting logic for the player
        if(canSelectKeys && Input.GetButtonDown("Fire1") && !gameBeaten)
        {
        	selectingKeys = true;
        	canSelectKeys = false;
        	contextClueA.SetActive(false);
        	KeyController.instance.DisplayKeys();
        }

        //Handles the spraying of the Bug Spray
        if(!Input.GetButton("Fire1") && hasBugSpray)
        {
        	isSpraying = false;
        }

        if(theSR.flipX == true && hasBugSpray && Input.GetButton("Fire1") && !isSpraying)
        {
        	bugSprayLeft.SetActive(true);
        	isSpraying = true;
        } else if(theSR.flipX == false && hasBugSpray && Input.GetButton("Fire1") && !isSpraying)
        {
        	bugSprayRight.SetActive(true);
        	isSpraying = true;
        } else if(!Input.GetButton("Fire1"))
        {
        	bugSprayRight.SetActive(false);
        	bugSprayLeft.SetActive(false);
        } 

        //Spawn the enemy if the player is taking too long
        if(spawnEnemy > 0)
        {
        	spawnEnemy -= Time.deltaTime;
        	if(spawnEnemy <= 0)
        	{
        		tentacleEnemy.SetActive(true);
        	}
    	}
    }


    //Move the player in a particular direction to loop the staircase
    private void OnTriggerEnter2D(Collider2D other)
    {
    	if(other.tag == "TopPoint" && !hasMoved && !gameBeaten)
    	{
    		theClocks.SetActive(true);
    		hasMoved = true;
    		transform.position = new Vector3(bottomPoint.position.x, bottomPoint.position.y, transform.position.z);
    		numCycles += 1;
    		keyToUse = ((numCycles/5f) - Mathf.Floor(numCycles/5f))*5f;
    		if(keyToUse == 0f)
    		{
    			keyToUse = 5f;
    		}
    		if(numCycles >= 5)
    		{
    			presserPuzzle.SetActive(true);
    			ButtonPresserController.instance.ResetButtons();

    		}
    		if(beginChestPuzzle)
    		{
    			chestPuzzle.SetActive(true);
    			chestPuzzleKeyring.SetActive(true);
    			ChestResetManager.instance.ResetChestPuzzle();
    		}

    	} else if(other.tag == "BottomPoint" && !hasMoved)
    	{
    		theClocks.SetActive(true);
    		hasMoved = true;
    		transform.position = new Vector3(topPoint.position.x, topPoint.position.y, transform.position.z);
    		numCycles +=1;
    		keyToUse = ((numCycles/5f) - Mathf.Floor(numCycles/5f))*5f;
    		if(keyToUse == 0f)
    		{
    			keyToUse = 5f;
    		}
    				
    		if(numCycles >= 5)
    		{
    			presserPuzzle.SetActive(true);
    			ButtonPresserController.instance.ResetButtons();

    		}
    		if(beginChestPuzzle)
    		{
    			chestPuzzle.SetActive(true);
    			chestPuzzleKeyring.SetActive(true);
    			ChestResetManager.instance.ResetChestPuzzle();
    		} 

    	}

    	if(other.tag == "KeyRing" && ChestController.instance.chestAttempted == false && !gameBeaten)
    	{
    		canSelectKeys = true;
    		contextClueA.SetActive(true);
    	}



    }

    private void OnTriggerExit2D(Collider2D other)
    {
    	if(other.tag == "KeyRing" && !gameBeaten)
    	{
    		canSelectKeys = false;
    		contextClueA.SetActive(false);
    		KeyController.instance.StopDisplayingKeys();
    	}
    }

    private void ResetChestPuzzle()
    {

    }
}
