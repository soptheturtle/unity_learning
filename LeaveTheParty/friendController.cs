using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class friendController : MonoBehaviour
{
    

	public GameObject theFriend;
	public Transform thePlayer;
	public Transform[] patrolPoints;
	public Transform exitPoint;
	public int i;
	public bool movingForward;

	public float friendSpeed, friendSpeedMultiplier;


	public float minWaitTime, maxWaitTime;
	private float waitTime;
	public bool isMoving;

	public float questionDistance, actionDistance;
	public GameObject questionMark, exclamationMark;
	public bool actionShown;

	public GameObject[] goodDialogue;
	public GameObject[] mediumDialogue;
	public GameObject[] badDialogue;
	public GameObject takeCareDialogue;
	public GameObject dontLeaveDialogue;


	public GameObject timingBarGame;
	public bool timingGameStarted, timingGameFinished;
	public float exitDistance;




	public GameObject happyFriend, sadFriend;
	public bool showSadFace;
	public bool takeCareShow;




    // Start is called before the first frame update
    void Start()
    {
        movingForward = true;
        isMoving = true;
        i = 0;
        timingGameStarted = false;
        timingGameFinished = false;
        actionShown = false;
        showSadFace = false;
        takeCareShow = true;

    }

    // Update is called once per frame
    void LateUpdate()
    {
        
    	//Move Back and Forth between the different patrol points
    	if(movingForward && isMoving && !timingGameStarted && !timingGameFinished)
    	{
    		theFriend.transform.position = Vector3.MoveTowards(theFriend.transform.position,patrolPoints[i].position, friendSpeed*Time.deltaTime);
    		if(Vector3.Distance(theFriend.transform.position,patrolPoints[i].transform.position) <= 0.1f)
    		{
    			i++;
    			isMoving = false;
    			waitTime = Random.Range(minWaitTime, maxWaitTime);
    			if(i == patrolPoints.Length)
    			{
    				movingForward = false;
    				i--;
    			}
    		}
    	} else if(isMoving && !timingGameStarted && !timingGameFinished)
    	{
    		theFriend.transform.position = Vector3.MoveTowards(theFriend.transform.position,patrolPoints[i].position, friendSpeed*Time.deltaTime);
    		if(Vector3.Distance(theFriend.transform.position,patrolPoints[i].transform.position) <= 0.1f)
    		{
    			i--;
    			isMoving = false;
    			waitTime = Random.Range(minWaitTime, maxWaitTime/2f);
    			if(i == -1)
    			{
    				movingForward = true;
    				i++;
    			}
    		}
    	}

    	//Waits here to decid when to start moving again
    	if(!isMoving)
    	{
    		waitTime -= Time.deltaTime;
    		if(waitTime < 0)
    		{
    			isMoving = true;
    		}
    	}


 		//Displays Quesiton Mark or Exclamation Mark
 		
    	if(!actionShown)
    	{
    	if(Vector3.Distance(theFriend.transform.position,thePlayer.position) <= actionDistance)
    	{
    		partyAudioController.instance.PlaySFX(1);
    		questionMark.SetActive(false);
    		exclamationMark.SetActive(true);
    		actionShown = true;
    		
    		
    	} else if(Vector3.Distance(theFriend.transform.position,thePlayer.position) <= questionDistance)
    	{
    		exclamationMark.SetActive(false);
    		questionMark.SetActive(true);
    		actionShown = false;
    	} else
    	{
    		questionMark.SetActive(false);
    		actionShown = false;
    	}
    	}

    	//Rushes to player to start the game
    	if(actionShown && timingGameStarted == false)
    	{
    		
    		//Move to the player
    		if(Vector3.Distance(theFriend.transform.position,thePlayer.position) >= 1.5f)
    		{
    			theFriend.transform.position = Vector3.MoveTowards(theFriend.transform.position,thePlayer.position,friendSpeed*friendSpeedMultiplier*Time.deltaTime);
    		}
    		else
    		{
    			exclamationMark.SetActive(false);
    			timingGameStarted = true;
    			timingBarGame.SetActive(true);
    			timingGameController.instance.StartTimingGame();
    		}
    	}

    	
    	//Handling timing game controller
    	if(timingGameStarted && !timingGameFinished)
    	{
    		if(Input.GetButtonDown("Fire1") && timingGameController.instance.gameCounter != 2)
    		{
    			if(timingGameController.instance.resultHit == 0)
    			{
    				showSadFace = true;
    				badDialogue[Random.Range(0,badDialogue.Length)].SetActive(true);

    			} else if(timingGameController.instance.resultHit == 1)
    			{
    				mediumDialogue[Random.Range(0,mediumDialogue.Length)].SetActive(true);
    			} else if(timingGameController.instance.resultHit == 2)
    			{
    				goodDialogue[Random.Range(0,goodDialogue.Length)].SetActive(true);
    			}	
    		}

    		if(timingGameController.instance.gameCounter == 2)
    		{
    			timingGameFinished = true;
    			timingGameController.instance.DeactivateTimingGameCo();
    			if(!showSadFace && takeCareShow)
    			{
    				takeCareShow = false;
    				TakeCareFriendCo();
    			} else
    			{
    				takeCareShow = false;
    			}
    			//Show sad or happy face!
    			ShowSadOrHappyCo();
    		}

    		if(Vector3.Distance(theFriend.transform.position,thePlayer.position) >= exitDistance && !timingGameFinished)
    		{
    			showSadFace = true;
    			playerController.instance.karmaPoints -= 10;
    			timingGameFinished = true;
    			dontLeaveDialogue.SetActive(true);
    			timingGameController.instance.DeactivateTimingGameCo();
    			ShowSadOrHappyCo();
    		}

    	}

    	//Handling Behavior once the timing game is over
    	if(timingGameFinished)
    	{
    		
 			
    		//Move the Friend to the exit point
    		theFriend.transform.position = Vector3.MoveTowards(theFriend.transform.position, exitPoint.position, friendSpeed*(1/friendSpeedMultiplier)* Time.deltaTime);


    		//Show the last line of Dialogue if player interacts again
    		if(Vector3.Distance(theFriend.transform.position, thePlayer.position) <= 1.5f)
    		{
    			if(showSadFace && Input.GetButtonDown("Fire1"))
    			{
    				badDialogue[Random.Range(0,badDialogue.Length)].SetActive(true);
    			} else if(Input.GetButtonDown("Fire1") && Vector3.Distance(theFriend.transform.position,exitPoint.position) <= 0.1f)
    			{
    				
    				takeCareDialogue.SetActive(true);
    			}
    		}


    	}


    }

        private void TakeCareFriendCo()
    	{
    		StartCoroutine(TakeCareFriend());
    	}

    	private IEnumerator TakeCareFriend()
    	{
    		yield return new WaitForSeconds(0.3f);
    		takeCareDialogue.SetActive(true);

    	}

    	private void ShowSadOrHappyCo()
    	{
    		StartCoroutine(ShowSadOrHappy());
    	}

    	private IEnumerator ShowSadOrHappy()
    	{
    		
    		yield return new WaitForSeconds(3.0f);
    		if(showSadFace)
    		{
    			sadFriend.SetActive(true);
    		} else
    		{
    			happyFriend.SetActive(true);
    		}

    	}



}
