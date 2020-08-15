using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightCameraController : MonoBehaviour
{
 
	
	//Set up what the Camera Controller is going to follow
	public static lightCameraController instance;
    public Transform target;

    
    //Making sure the camera doesn't go too far in any given direction
    public float minHeight, maxHeight, minHorizontal, maxHorizontal;
    private Vector2 lastPos;
    public bool cameraTrip;
    public Transform[] cameraPoints;

    public float cameraSpeed;
    private int cameraCurrentPointTarget;


     void Awake()
     {
     	instance = this;
     }

    // Start is called before the first frame update
    void Start()
    {
	    lastPos = new Vector2(target.position.x, target.position.y);
	    cameraTrip = false;
	    cameraCurrentPointTarget = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(cameraTrip == false)
        {
        	transform.position = new Vector3(Mathf.Clamp(target.position.x, minHorizontal,maxHorizontal), Mathf.Clamp(target.position.y, minHeight, maxHeight), transform.position.z);
    	}
    	else if(cameraCurrentPointTarget <= 7)
    	{
    		
    		if(cameraCurrentPointTarget == 0)
    		{
    		gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, cameraPoints[cameraCurrentPointTarget].position,cameraSpeed * Time.deltaTime * 20f);
    		}
    		else 
    		{
    		gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, cameraPoints[cameraCurrentPointTarget].position,cameraSpeed * Time.deltaTime);	
    		}
    		if(Vector3.Distance(gameObject.transform.position, cameraPoints[cameraCurrentPointTarget].position) < 0.1f)
    		{
    			cameraCurrentPointTarget += 1;
    			//Prevents a Vector Overflow error
    			if(cameraCurrentPointTarget > 7)
    			{
    				cameraCurrentPointTarget = 7;
    				lightLevelManager.instance.LoadTheEnd();
    			}
    		}
    	}
    }
}
