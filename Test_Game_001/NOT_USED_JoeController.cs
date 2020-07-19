using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class JoeController : MonoBehaviour
{
	
	public static JoeController instance;


	public float moveSpeed;
	private float currentMoveSpeed;
	public Rigidbody2D theRB;
	
	
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
        swordEndPoint.position = new Vector3(0f,0f,0f);
        swordActive = false;
        //swordEndPoint.position = new Vector3(0f,0f,0f);
    }

    // Update is called once per frame
    void Update()
    {
    	
    	
    	//Player Controller
    	if(Input.GetButtonDown("Jump"))
    	{
    		Debug.Log("We are running!");
    		currentMoveSpeed *= runMultiplier;
    	} 
    	
    	if(Input.GetButtonUp("Jump"))
    	{
    		currentMoveSpeed = moveSpeed;
    	}
        
    	
        theRB.velocity = new Vector2(currentMoveSpeed * Input.GetAxis("Horizontal") , currentMoveSpeed * Input.GetAxis("Vertical") * runtoApply);


        //Sword Controller
        if(Input.GetButtonDown("Fire1") && swordEndPoint.position == Vector3.zero)
        {
        	
        	Debug.Log("Sword On");
        	playerSword.SetActive(true);
        	swordStartPoint.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 0.25f,gameObject.transform.position.z);
        	swordEndPoint.position = new Vector3(swordStartPoint.position.x + swordDistance, swordStartPoint.position.y, swordStartPoint.position.z);
        	swordActive = true;
        }

        if(swordEndPoint.position != Vector3.zero)
        {
        	Debug.Log("Sword X Point: " + playerSword.transform.position.x);
        	Debug.Log("Sword End Point: " + swordEndPoint.position.x);
    	}
        
        if(swordActive = true && Vector3.Distance(playerSword.transform.position, swordEndPoint.position) < 0.1f)
        {
        	playerSword.SetActive(false);
        	swordEndPoint.position = Vector3.zero;
        	playerSword.transform.position = swordStartPoint.position;
        	swordActive = false;
        } else if(swordEndPoint.position != Vector3.zero)
        {
        	playerSword.transform.position = Vector3.MoveTowards(playerSword.transform.position, swordEndPoint.position, swordSpeed * Time.deltaTime);
        }

        
        
	}

   
}
