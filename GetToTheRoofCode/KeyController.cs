using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    
	public static KeyController instance;

	public SpriteRenderer keyRenderer;
	public GameObject keyDisplayer;

	public Sprite[] theKeys;
	private int keyPos;

	public float coolDownSwitcher;
	private float currentCountdown;
	public bool startSelecting;


	void Awake()
	{
		instance = this;
		startSelecting = false;
	}

    // Start is called before the first frame update
    void Start()
    {
        keyPos = 0;
        keyRenderer.sprite = theKeys[keyPos];
        currentCountdown = coolDownSwitcher;

    }

    // Update is called once per frame
    void Update()
    {
        

    	//Actually select the key you want to use
    	//The countdown is for dealing issue with the GetAxis stuff. 
    	//I haven't figured out how to get left/right to work as a button press
    	if(currentCountdown < 0f)
    	{
	        if(Input.GetAxis("Horizontal") > 0 && PlayerController.instance.selectingKeys)
	        {
	        	keyPos +=1;
	        	if(keyPos > 4)
	        	{
	        		keyPos = 0;
	        	}
	        	keyRenderer.sprite = theKeys[keyPos];
	        	currentCountdown = coolDownSwitcher;
	        } else if(Input.GetAxis("Horizontal") < 0 && PlayerController.instance.selectingKeys)
	        {
	        	keyPos -=1;
	        	if(keyPos < 0)
	        	{
	        		keyPos = 4;
	        	}
	        	keyRenderer.sprite = theKeys[keyPos];
	        	currentCountdown = coolDownSwitcher;
	        }
	    }

	    currentCountdown -= Time.deltaTime;


        //Take the selected key
        if(Input.GetButtonDown("Fire1") && startSelecting)
        {
        	PlayerController.instance.selectedKey = keyPos + 1;
        	PlayerController.instance.selectingKeys = false;
			startSelecting = false;
        	StopDisplayingKeys();
        	
        	
        }




    }


    public void DisplayKeys()
    {
    	keyDisplayer.SetActive(true);
    	startSelecting = true;
    }

    public void StopDisplayingKeys()
    {
    	keyDisplayer.SetActive(false);
    }
}
