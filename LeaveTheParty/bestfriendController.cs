using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bestfriendController : MonoBehaviour
{
    

	public Transform[] patrolPoints;
	public Transform exitPoint;
	private int i,j;


	public float waitTime;
	private float waitTimeRemaining;
	public GameObject bestFriend;
	public GameObject aButton;
	public GameObject[] bestfriendDialogue;
	public GameObject questionMark, exclamationMark;
	public Transform thePlayerPosition;

	public float questionDistance, actionDistance;

	public bool questionShown, actionShown, dialogueFinished, newDialogueStart;

	public float dialogueTime;
	private float dialogueTimeRemaining;

	public float friendSpeed, friendSpeedMultiplier;

    // Start is called before the first frame update
    void Start()
    {
     questionShown = false;
     actionShown = false;
     dialogueFinished = false;   
     waitTimeRemaining = waitTime;
     dialogueTimeRemaining = dialogueTime;
     newDialogueStart = false;
     i = 0;
     j = 0;
     
    }

    // Update is called once per frame
    void Update()
    {
        
        //The Friend moves back and forth between a set of points here
        if(!actionShown)
        {
        if(Vector3.Distance(bestFriend.transform.position, patrolPoints[i].position) <= 0.05f && waitTimeRemaining > 0)
        {
        	waitTimeRemaining -= Time.deltaTime;
        	if(waitTimeRemaining <= 0)
        	{
        		waitTimeRemaining = waitTime;
        		i = i + 1;
        		if(i==patrolPoints.Length)
        		{
        			i=0;
        		}
        	}
    	}  else if(dialogueFinished)
    	{
    		bestFriend.transform.position = Vector3.MoveTowards(bestFriend.transform.position, exitPoint.position, friendSpeed * Time.deltaTime);
    	} else

    	{
    		bestFriend.transform.position = Vector3.MoveTowards(bestFriend.transform.position, patrolPoints[i].position, friendSpeed * Time.deltaTime);
    	}
    	}

    	//Goodbye text for if the player tries to talk to this player again
    	if(dialogueFinished && Vector3.Distance(bestFriend.transform.position, thePlayerPosition.position) <= 1.5f && Input.GetButtonDown("Fire1") && !newDialogueStart)
    	{
    		bestfriendDialogue[bestfriendDialogue.Length - 1].SetActive(true);
    		aButton.SetActive(false);
    		newDialogueStart = true;

    	} else if(newDialogueStart)
    	{
    		dialogueTimeRemaining -= Time.deltaTime;
    		if(dialogueTimeRemaining <= 0f)
    		{
    			dialogueTimeRemaining = dialogueTime;
    			bestfriendDialogue[bestfriendDialogue.Length - 1].SetActive(false);
    			newDialogueStart = false;
    		}
    	}


    	//Move to the player and begin showing the dialogue
    	if(actionShown)
    	{
    		if(Vector3.Distance(bestFriend.transform.position, thePlayerPosition.position) >= 1.5f)
    		{
    		bestFriend.transform.position = Vector3.MoveTowards(bestFriend.transform.position,thePlayerPosition.position, friendSpeed*friendSpeedMultiplier*Time.deltaTime);
    		} else
    		{
    			exclamationMark.SetActive(false);
    			bestfriendDialogue[j].SetActive(true);
    			if(Input.GetButtonDown("Fire1"))
    			{
    				
    				bestfriendDialogue[j].SetActive(false);
    				j++;
    				if(j == bestfriendDialogue.Length)
    				{
    					actionShown = false;
    					playerController.instance.canMove = true;
    					dialogueFinished = true;
    				}
    			}


    		}

    	}


    	//Set up the exclamation and question block being shown
    	if(!actionShown && j != bestfriendDialogue.Length)
    	{
    	if(Vector3.Distance(bestFriend.transform.position,thePlayerPosition.position) <= actionDistance)
    	{
	   		
    		partyAudioController.instance.PlaySFX(1);
	   		questionMark.SetActive(false);
    		exclamationMark.SetActive(true);
    		actionShown = true;
    		playerController.instance.canMove = false;
    		partyAudioController.instance.StopSFXWalking();

    	} else if(Vector3.Distance(bestFriend.transform.position,thePlayerPosition.position) <= questionDistance)
    	{
    		exclamationMark.SetActive(false);
    		questionMark.SetActive(true);
    	} else
    	{
    		questionMark.SetActive(false);
    	}
    	}

    }
}
