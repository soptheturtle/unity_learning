using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    

	
	public Transform playerPosition;
	public float moveSpeed, moveSpeedIncrementer, maxSpeed;
	private float currentMoveSpeed;
	public float speedUpTimer;
	private float currentTimer;
	private Rigidbody2D theRB;
	private Vector3 oldPos;
	private SpriteRenderer theSR;
	private bool isBeaten;




    // Start is called before the first frame update
    void Start()
    {
        currentMoveSpeed = moveSpeed;
        currentTimer = speedUpTimer;
        theRB = GetComponent<Rigidbody2D>();
        theSR = GetComponent<SpriteRenderer>();
        oldPos = transform.position;
        isBeaten = false;
    }

    // Update is called once per frame
    void Update()
    {
        
         // Vector2 moveDirection = gameObject.rigidbody2D.velocity;
         // if (moveDirection != Vector2.zero) {
         //     float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
         //     transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
         // }

        if(!isBeaten)
        {
        	transform.position = Vector3.MoveTowards(transform.position, playerPosition.position, currentMoveSpeed * Time.deltaTime);	
        } else if(isBeaten)
        {
        	theRB.velocity = new Vector2(0f, -currentMoveSpeed*3f);
        }
        
    	if(oldPos.x > transform.position.x)
    	{
    		theSR.flipX = true;
    	} else
    	{
    		theSR.flipX = false;
    	}
    	oldPos = transform.position;
    

    	//Handle the timing for when the 
    	//tentacle enemy speeds up if the
    	//player is taking their sweet time
        currentTimer -= Time.deltaTime;
        if(currentTimer <0f)
        {
        	currentMoveSpeed *= moveSpeedIncrementer;
        	currentTimer = speedUpTimer;
        }
        if(currentMoveSpeed > maxSpeed)
        {
        	currentMoveSpeed = maxSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
    	//End the Game if player touches
    	if(other.tag == "Player")
    	{
    		LevelManager.instance.EndGame();
    	}

    	//Defeating the tentacle boss
    	if(other.tag =="BugSpray")
    	{
    		isBeaten = true;
    		PlayerController.instance.gameBeaten = true;
    	}
    }
}
