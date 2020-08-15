using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightPlayerController : MonoBehaviour
{
    

	public static lightPlayerController instance;
	public float moveSpeed;
	private float currentMoveSpeed;
	public Rigidbody2D theRB;
	private bool isRunning;
	private Animator anim;
	private SpriteRenderer theSR;
	public float runMultiplier;

	public GameObject[] lightsToInstantiate;
	public GameObject lightToInstantiate;
	public float timeBetweenPlacements;
	private float lightPlacementCountdown;
	public float amountToPlace;

	private int moveDirection;
	public bool canPlace;
	public bool canMove;

	public int pieceLimit;

	public Transform playerStartPoint;
	public float fallOfftime;
	public float fallOffSpeed;
	public bool isFalling;

	public int playerPoints;

	public GameObject goodJob;
	public float goodJobTime;
	public bool goodJobCanPlace;

	


	void Awake()
	{
		instance = this;

	}

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        theSR = GetComponent<SpriteRenderer>();
        currentMoveSpeed = moveSpeed;
        moveDirection = 0; //0, 1 = up, 2 = left, 3 = down, 4 = right
        canPlace = true;
        canMove = true;
        lightUIController.instance.pieceChosen = 0;
        isFalling = false;
        playerPoints = 0;
        goodJobCanPlace = true;
        goodJobTime = 0;
    }

    // Update is called once per frame
    void LateUpdate()
    {

    	//Handle good animation
    	if(goodJobCanPlace == false)
    	{
    		goodJobTime -= Time.deltaTime;
    		if(goodJobTime <= 0.05f)
    		{
    			goodJobCanPlace = true;
    		}
    	}

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
    	
       
       //Controls for checking all 4 components of animator
       if(canMove)
       {
	       if(Input.GetAxis("Vertical") < -0.1 && Mathf.Abs(Input.GetAxis("Vertical")) > Mathf.Abs(Input.GetAxis("Horizontal")))
	       {
	       	moveDirection = 3;
	       }
	       else if(Input.GetAxis("Vertical") > 0.1 && Mathf.Abs(Input.GetAxis("Vertical")) > Mathf.Abs(Input.GetAxis("Horizontal")))
	       {
			moveDirection = 1;
	       }
	       else if(Input.GetAxis("Horizontal") < -0.1)
	       {
	       	moveDirection = 2;
	       }
	       else if(Input.GetAxis("Horizontal") > 0.1)
	       {
			moveDirection = 4;
	       } else
	       {
	       	moveDirection = 0;
	       }
	    	
	    	anim.SetInteger("MoveDirection", moveDirection);
	       	theRB.velocity = new Vector2(currentMoveSpeed * Input.GetAxis("Horizontal") , currentMoveSpeed * Input.GetAxis("Vertical"));
	    }
	    //How the player should behave when things are done
	    if(lightCameraController.instance.cameraTrip == true)
	    {
	    	anim.SetInteger("MoveDirection", 0);
	    	theRB.velocity = new Vector2(0f,0f);
	    }

       	

       	//***************
       	//Piece Placement
       	//***************
       	//The UI controls what piece can be placed
    	if(lightUIController.instance.pieceChosen == 0)
    	{
    		lightToInstantiate = lightsToInstantiate[0];
    	} else
    	{
    		lightToInstantiate = lightsToInstantiate[lightUIController.instance.pieceChosen -1];
    	}

       	//Only allowed to do anything with placing pieces if there are still pieces left!
       	if(pieceLimit > 0 && canMove)
       	{
	       	if(Input.GetButton("Fire1") && lightPlacementCountdown <= 0.05 && canPlace == true && Mathf.Abs(theRB.velocity.x) < 0.01 && Mathf.Abs(theRB.velocity.y) < 0.01)
	       	{
	       		
	       		Instantiate(lightToInstantiate, new Vector3(transform.position.x, transform.position.y - amountToPlace, transform.position.z), transform.rotation);
	       		lightAudioManager.instance.PlaySFX(0);
	       		lightPlacementCountdown = timeBetweenPlacements;
	       		canPlace = false;
	       		pieceLimit -= 1;
	       		playerPoints += 1;
	       	}

	       	if(lightPlacementCountdown > 0)
	       	{
	       		lightPlacementCountdown -= Time.deltaTime;
	       	}
        }

    }


    //Don't allow placement if on top of a light already
    private void OnTriggerEnter2D(Collider2D other)
   	{
    	

    	if(other.gameObject.tag == "XmasLight" && other.transform.position.y < transform.position.y)
    	{
    		
    		canPlace = false;
    	} else if(other.gameObject.tag == "FallPlane")
    	{
    		StartCoroutine(FallOffRoofCo());
    	}
    }

    private void OnTriggerExit2D(Collider2D other)
    {
    	

    	if(other.gameObject.tag == "XmasLight")
    	{
    		canPlace = true;
    	}
    }


    private IEnumerator FallOffRoofCo()
    {
    	
    	if(!isFalling)
    	{
    	isFalling = true;
    	canMove = false;
    	if(pieceLimit - 10 < 0)
    	{
    		pieceLimit = 0;
    	} else
    	{
    		pieceLimit -= 10;
    		playerPoints -= 10;
    	}
    	theRB.velocity = new Vector2(0f,0f);
    	anim.SetTrigger("PlayerHurt");
    	lightAudioManager.instance.PlaySFX(1);
    	yield return new WaitForSeconds(fallOfftime);
    	theRB.velocity = new Vector2(0f, -fallOffSpeed);
		yield return new WaitForSeconds(0.25f);
    	theSR.color = new Color(1f,1f,1f,0f);
    	yield return new WaitForSeconds(fallOfftime + 0.5f);
    	theRB.velocity = new Vector2(0f, 0f);
    	gameObject.transform.position = new Vector3(playerStartPoint.position.x, playerStartPoint.position.y, gameObject.transform.position.z);
    	theSR.color = new Color(1f,1f,1f,1f);

    	canMove = true;
    	isFalling = false;
    	}
    	


    }
    public IEnumerator LightCrushed()
    {
    	canMove = false;
    	anim.SetTrigger("PlayerHurt");
    	theRB.velocity = Vector2.zero;
    	lightAudioManager.instance.PlaySFX(1);
    	yield return new WaitForSeconds(2.5f);
    	canMove = true;
    }

    public void CreateGoodTime()
    {
    	if(goodJobCanPlace)
    	{
    	Instantiate(goodJob,new Vector3(transform.position.x, transform.position.y + amountToPlace + 1.6f, transform.position.z), transform.rotation);
    	goodJobCanPlace = false;
    	goodJobTime = 1.0f;
    	}
    }



}
