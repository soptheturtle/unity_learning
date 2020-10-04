using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPresserController : MonoBehaviour
{
    
	public static ButtonPresserController instance;
	public GameObject[] button;
	public GameObject playerContext;
	public bool[] buttonPressed;
	public bool[] correctPress;
	public SpriteRenderer[] theSR;
	public Sprite  litSprite,darkSprite;
	public float distanceToPress;
	public GameObject tentacleEnemy;

	void Awake()
	{
		instance = this;
	}


    // Start is called before the first frame update
    void Start()
    {
		ResetButtons();
    }

    // Update is called once per frame
    void Update()
    {
        
    	//Context Button highlighting
    	float dist1 = Vector3.Distance(PlayerController.instance.transform.position,button[0].transform.position);
    	float dist2 = Vector3.Distance(PlayerController.instance.transform.position,button[1].transform.position);
    	float dist3 = Vector3.Distance(PlayerController.instance.transform.position,button[2].transform.position);

    	if(dist1 <= distanceToPress && !buttonPressed[0])
    	{
    		playerContext.SetActive(true);
    	} else if (dist2 <= distanceToPress && !buttonPressed[1])
    	{
    		playerContext.SetActive(true);
    	} else if (dist3 <= distanceToPress && !buttonPressed[2])
    	{
    		playerContext.SetActive(true);
    	} else
    	{
    		playerContext.SetActive(false);
    	}

    	//Button 1 Rules
        if(Vector3.Distance(PlayerController.instance.transform.position,button[0].transform.position) <= distanceToPress && Input.GetButtonDown("Fire1") && PlayerController.instance.beginChestPuzzle == false)
        {
        	theSR[0].sprite = litSprite;
        	buttonPressed[0] = true;
        	if(buttonPressed[2] == true && buttonPressed[1] == false)
        	{
        		correctPress[0] = true;
        	}
        }

       //Button 2 Rules
       if(Vector3.Distance(PlayerController.instance.transform.position,button[1].transform.position) <= distanceToPress && Input.GetButtonDown("Fire1") && PlayerController.instance.beginChestPuzzle == false)
        {
        	theSR[1].sprite = litSprite;
        	buttonPressed[1] = true;
        	if(buttonPressed[0] == true && buttonPressed[2] == true)
        	{
        		correctPress[1] = true;
        	}
        }

       //Button 3 Rules
       if(Vector3.Distance(PlayerController.instance.transform.position,button[2].transform.position) <= distanceToPress && Input.GetButtonDown("Fire1") && PlayerController.instance.beginChestPuzzle == false)
        {
        	theSR[2].sprite = litSprite;
        	buttonPressed[2] = true;
        	if(buttonPressed[0] == false && buttonPressed[1] == false)
        	{
        		correctPress[2] = true;
        	}
        }

        if(correctPress[0] && correctPress[1] && correctPress[2])
        {
        	PlayerController.instance.beginChestPuzzle = true;
        	tentacleEnemy.SetActive(true);
        } else if (buttonPressed[0] && buttonPressed[1] && buttonPressed[2] && (!correctPress[0] || !correctPress[1] || !correctPress[2]))
        {
        	PlayerController.instance.beginChestPuzzle = false;
        }




    }

       public void ResetButtons()
        {
	       	if(PlayerController.instance.beginChestPuzzle == false)
	       	{
		        buttonPressed[0] = false;
		        buttonPressed[1] = false;
		        buttonPressed[2] = false;
		        correctPress[0] = false;
		        correctPress[1] = false;
		        correctPress[2] = false;
		        theSR[0].sprite = darkSprite;
		        theSR[1].sprite = darkSprite;
		        theSR[2].sprite = darkSprite;
	    	}
        }
}
