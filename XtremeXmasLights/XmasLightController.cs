using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XmasLightController : MonoBehaviour
{

	//I should've used vectors this is REALLY BAD code. DO NOT COPY! Use
	//The camera controller's camera points method instead
	//Joseph Garland (@soptheturtle on twitter) wrote this crazy code    
	
	//Darkened sprites
	public Sprite s1, s2, s3, s4, s5, s6, s7, s8;
	private Sprite darkSprite;
	
	//Lit sprites
	public Sprite sl1, sl2, sl3, sl4, sl5, sl6, sl7, sl8;
	private Sprite lightSprite;
	
	//Broken sprites
	public Sprite sb1, sb2, sb3, sb4, sb5, sb6, sb7, sb8;
	private Sprite brokenSprite;

	public float distanceAllowed;
	public bool isCrushed;
	
	//Light controllers for flickering
	public float flickerTime;
	private float flickerTimeRemaining;
	public bool litPiece;
	public int flickerInterval;
	
	public GameObject thePlayer;

	public SpriteRenderer theSR;

	public int spriteToRender;

	public bool lightShow; //For the final camera pan around

    // Start is called before the first frame update
    
	void Awake()
	{
		if(litPiece)
		{
		spriteToRender = Random.Range(1,9);
		flickerInterval = Random.Range(1,3);
		if(spriteToRender > 8)
		{
			spriteToRender = 8;
		}
		}
		isCrushed = false;

	}

    void Start()
    {
        
        theSR = GetComponent<SpriteRenderer>();
        flickerTimeRemaining = flickerTime;
    }

    // Update is called once per frame
    void Update()
    {
     
    //Handle the flicking lights

     if(lightCameraController.instance.cameraTrip == true && !isCrushed)
     {   	
     	flickerTimeRemaining -= Time.deltaTime * (flickerInterval/3f);
     	if(flickerTimeRemaining < 0.05f)
     	{
     		flickerTimeRemaining = flickerTime;
     	}
     		//Sprites are dark
     		if(flickerTimeRemaining > flickerTime/2f)
     		{
     		switch(spriteToRender)
     		{
     			case 1:
     			theSR.sprite = s1;
     			break;
     			case 2:
     			theSR.sprite = s2;
     			break;
     			case 3:
     			theSR.sprite = s3;
     			break;
     			case 4:
     			theSR.sprite = s4;
     			break;
     			case 5:
     			theSR.sprite = s5;
     			break;
     			case 6:
     			theSR.sprite = s6;
     			break;
     			case 7:
     			theSR.sprite = s7;
     			break;
     			case 8:
     			theSR.sprite = s8;
     			break;
     		}
     		} else
     		{
     		switch(spriteToRender)
     		{
     			case 1:
     			theSR.sprite = sl1;
     			break;
     			case 2:
     			theSR.sprite = sl2;
     			break;
     			case 3:
     			theSR.sprite = sl3;
     			break;
     			case 4:
     			theSR.sprite = sl4;
     			break;
     			case 5:
     			theSR.sprite = sl5;
     			break;
     			case 6:
     			theSR.sprite = sl6;
     			break;
     			case 7:
     			theSR.sprite = sl7;
     			break;
     			case 8:
     			theSR.sprite = sl8;
     			break;
     		}
     		}
     	
     } else
     {
     if(!isCrushed)
     {
     switch(spriteToRender)
     {
     	case 1:
     	theSR.sprite = s1;
     	break;
     	case 2:
     	theSR.sprite = s2;
     	break;
     	case 3:
     	theSR.sprite = s3;
     	break;
     	case 4:
     	theSR.sprite = s4;
     	break;
     	case 5:
     	theSR.sprite = s5;
     	break;
     	case 6:
     	theSR.sprite = s6;
     	break;
     	case 7:
     	theSR.sprite = s7;
     	break;
     	case 8:
     	theSR.sprite = s8;
     	break;
     }
 	 }
 	 else if(isCrushed)
 	 {
     switch(spriteToRender)
     {
     	case 1:
     	theSR.sprite = sb1;
     	break;
     	case 2:
     	theSR.sprite = sb2;
     	break;
     	case 3:
     	theSR.sprite = sb3;
     	break;
     	case 4:
     	theSR.sprite = sb4;
     	break;
     	case 5:
     	theSR.sprite = sb5;
     	break;
     	case 6:
     	theSR.sprite = sb6;
     	break;
     	case 7:
     	theSR.sprite = sb7;
     	break;
     	case 8:
     	theSR.sprite = sb8;
     	break;
     }

 	 }

     if(Mathf.Abs(Vector3.Distance(transform.position, lightPlayerController.instance.transform.position)) <= distanceAllowed && lightPlayerController.instance.transform.position.y > transform.position.y)
     {

     	//Debug.Log("Setting False xmaslight");
     	lightPlayerController.instance.canPlace = false;
     }   
 	}
    }
}



