using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SmasherController : MonoBehaviour
{
    
	public Transform topPoint, bottomPoint;
	public float distanceToSlam;
	public float slamSpeed, liftSpeed;
	public bool hasSlammed;

	private bool keepSlamming;
	private float playerDistance;

	public GameObject smasherSprite;

	public float waitTime;
	private float currentWait;

    // Start is called before the first frame update
    void Start()
    {
        currentWait = waitTime;
    }

    // Update is called once per frame
    void Update()
    {
     
    	playerDistance = Mathf.Abs(smasherSprite.transform.position.x - PlayerController.instance.transform.position.x);
    			
		if((playerDistance < distanceToSlam && hasSlammed == false) || keepSlamming == true)
    	{  	
        	keepSlamming = true;
        	hasSlammed = false;
        	currentWait = waitTime;
        	smasherSprite.transform.position = Vector3.MoveTowards(smasherSprite.transform.position, bottomPoint.position, slamSpeed * Time.deltaTime);

        	
        	if(Vector3.Distance(smasherSprite.transform.position, bottomPoint.position) < 0.1f)
        	{
        		
        		keepSlamming = false;
        		hasSlammed = true;
        		
        	}
    	} else if(hasSlammed == true && keepSlamming == false)
    	{
    		
    		currentWait -= Time.deltaTime;
    		if(!(currentWait > 0))
    		{
    			smasherSprite.transform.position = Vector3.MoveTowards(smasherSprite.transform.position, topPoint.position, liftSpeed * Time.deltaTime);
    		}
    		
    		if(Vector3.Distance(smasherSprite.transform.position, topPoint.position) < 0.1f )
    		{
    			hasSlammed = false;
    			keepSlamming = false;
    		}
    	} else
    	{
    		currentWait = waitTime;
    	}

    }
}

