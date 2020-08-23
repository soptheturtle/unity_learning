using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timingGameController : MonoBehaviour
{
    

	public static timingGameController instance;
	public GameObject timingPointer;
	public Transform leftPoint,centerPoint,rightPoint;
	private bool movingLeft;

	public bool gameStarted;

	public int gameCounter;
	public int resultHit;


	public GameObject[] result1;
	public SpriteRenderer[] resultSR;
	public Sprite noResult, badResult, mediumResult, goodResult;

	public float goodThreshold, badThreshold;

	public float timerSpeed;


	void Awake()
	{
		instance = this;
		movingLeft = true;

	}

    // Start is called before the first frame update
    void Start()
    {
        gameStarted = true;
        resultSR[0] = result1[0].GetComponent<SpriteRenderer>();
        resultSR[1] = result1[1].GetComponent<SpriteRenderer>();
        resultSR[2] = result1[2].GetComponent<SpriteRenderer>();
        StartTimingGame();
        resultHit = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    	
    	//This moves the timing bar back and forth
    	if(movingLeft)
    	{
        	timingPointer.transform.position = Vector3.MoveTowards(timingPointer.transform.position, leftPoint.position, timerSpeed * Time.deltaTime);
        	if(Mathf.Abs(timingPointer.transform.position.x - leftPoint.position.x) <= 0.1f)
        	{
        		movingLeft = false;
        	}
    	} else
    	{
        	timingPointer.transform.position = Vector3.MoveTowards(timingPointer.transform.position, rightPoint.position, timerSpeed * Time.deltaTime);
        	if(Mathf.Abs(timingPointer.transform.position.x - rightPoint.position.x) <= 0.1f)
        	{
        		movingLeft = true;
        	}
    	}

    	
    	//Control what should be shown based on when player hits A
    	if(Input.GetButtonDown("Fire1") && gameStarted == true)
    	{
    		
    		gameCounter++;
    		
    		if(Mathf.Abs(timingPointer.transform.position.x - centerPoint.position.x) <= goodThreshold)
    		{
    			resultSR[gameCounter].sprite = goodResult;
    			resultHit = 2;
    			partyAudioController.instance.PlaySFX(0);
    		} else if (Mathf.Abs(timingPointer.transform.position.x - centerPoint.position.x) <= badThreshold)
    		{
    			resultSR[gameCounter].sprite = mediumResult;
    			playerController.instance.restPoints -=2;
    			resultHit = 1;
    			partyAudioController.instance.PlaySFX(1);
    		} else
    		{
    			resultSR[gameCounter].sprite = badResult;
    			playerController.instance.restPoints -= 5;
    			playerController.instance.karmaPoints -= 5;
    			resultHit = 0;
    			partyAudioController.instance.PlaySFX(2);
    		}

    		if(gameCounter == resultSR.Length - 1)
    		{
    			gameStarted = false;
    			DeactivateTimingGameCo();
    		}

    	}
    }

    public void StartTimingGame()
    {
    	gameStarted = true;
    	resultSR[0].sprite = noResult;
    	resultSR[1].sprite = noResult;
    	resultSR[2].sprite = noResult;
    	playerController.instance.restPoints -= 5;
    	gameCounter = -1;
    	resultHit = 0;

    }

    public void DeactivateTimingGameCo()
    {
    	StartCoroutine(DeactivateTimingGame());
    }

    public IEnumerator DeactivateTimingGame()
    {
    	

    	yield return new WaitForSeconds(0.75f);
    	gameCounter = -1;
    	gameObject.SetActive(false);

    }
}
