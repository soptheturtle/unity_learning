using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlimeController : MonoBehaviour
{
    

	public Transform attackTarget;
	public float minWaitTime;
	public float maxWaitTime;
	private float waitTime;
	public float minMoveTime;
	public float maxMoveTime;
	private float moveTime;
	public Transform slimeTarget;
	private bool attackPlayer;
	public Rigidbody2D theRB;
	private float slimeXSpeed;
	private float slimeYSpeed;
	public int swordDamage;

	private bool isMoving;
	public float slimeMoveSpeed;

	public int slimeHealth;
	public GameObject deathEffect;

    // Start is called before the first frame update
    void Start()
    {
        isMoving = false;
        waitTime = Random.Range(minWaitTime, maxWaitTime);
        slimeTarget.transform.position = new Vector3(gameObject.transform.position.x + Random.Range(-5f,5f) + gameObject.transform.position.y + Random.Range(-5f,5f), gameObject.transform.position.z);
        slimeXSpeed = Random.Range(-5f,5f);
        slimeYSpeed = Random.Range(-5f,5f);
    }

    // Update is called once per frame
    void Update()
    {
      
      //Control when the slime actually moves
    	if(waitTime >= 0)
    	{
      		waitTime -= Time.deltaTime;
      		isMoving = false;
      	} else if(isMoving == false)
      	{
      		isMoving = true;
      		moveTime = Random.Range(minMoveTime, maxMoveTime);
      		//50% change of moving to the player or something else
      		if(Random.Range(0.0f,1.0f) > 0.5)
      		{
      			attackPlayer = true;
      		} else
      		{
      			attackPlayer = false;
      			slimeTarget.transform.position = new Vector3(gameObject.transform.position.x + Random.Range(-5f,5f) + gameObject.transform.position.y + Random.Range(-5f,5f), gameObject.transform.position.z);
      			slimeXSpeed = Random.Range(-5f,5f);
        		slimeYSpeed = Random.Range(-5f,5f);

      		}
      	}

      	//Move the slime to the player
      	if(isMoving == true)
      	{
      	
      	if(attackPlayer == true)
      	{
      		gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, attackTarget.position, slimeMoveSpeed * Time.deltaTime);
      	} else
      	{
      		theRB.velocity = new Vector2(slimeXSpeed,slimeYSpeed);
      		//gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, slimeTarget.position, slimeMoveSpeed * Time.deltaTime);
      	}
      	moveTime -= Time.deltaTime;
      	
      	//When the time has elapsed for moving the slime will pause briefly
      	if(moveTime <= 0)
      	{
      		waitTime = Random.Range(minWaitTime, maxWaitTime);
      		theRB.velocity = new Vector2(0f, 0f);
      		isMoving = false;
      	}

      	}


    

    }

        public void OnCollisionEnter2D(Collision2D other)
    {
    	if(other.gameObject.tag == "Player")
    	{
    		isMoving = true;
    		moveTime = Random.Range(0.25f, 0.5f);
    		slimeXSpeed = Random.Range((gameObject.transform.position.x - other.gameObject.transform.position.x)*2f,(gameObject.transform.position.x - other.gameObject.transform.position.x)*5f);
    		slimeYSpeed = Random.Range((gameObject.transform.position.y - other.gameObject.transform.position.y)*2f,(gameObject.transform.position.y - other.gameObject.transform.position.y)*5f);
    		attackPlayer = false;
    		waitTime = -1;
    	}
    	}

    private void OnTriggerEnter2D(Collider2D other)
    {
    
    
    	if (other.gameObject.tag == "PlayerSword")
    	{
    		isMoving = true;
    		other.gameObject.SetActive(false);
    		moveTime = Random.Range(0.75f, 1.25f);
    		slimeXSpeed = Random.Range((gameObject.transform.position.x - other.gameObject.transform.position.x)*4f + 3f,(gameObject.transform.position.x - other.gameObject.transform.position.x)*7f+ 3f);
    		slimeYSpeed = Random.Range((gameObject.transform.position.y - other.gameObject.transform.position.y)*4f + 3f,(gameObject.transform.position.y - other.gameObject.transform.position.y)*7f + 3f);
    		attackPlayer = false;
    		slimeHealth -= swordDamage;
    		theRB.velocity = new Vector2(slimeXSpeed,slimeYSpeed);
    		waitTime = -1;
      	if(slimeHealth <= 0)
      	{
      		
      		Destroy(gameObject);

    		//The reason why I have to adjust the instantiation of the deathEffect is because
    		//it always creates bottom left of where it should and this is after
    		//resetting the transform of the prefab to zero,zero,zero. 
    		//But this works well enough.
    		Instantiate(deathEffect, new Vector3(gameObject.transform.position.x+0.82f,gameObject.transform.position.y+0.78f,gameObject.transform.position.z), gameObject.transform.rotation);
    		Debug.Log("Game Object: "+gameObject.transform.position);
      	}

    	}
    }


}
