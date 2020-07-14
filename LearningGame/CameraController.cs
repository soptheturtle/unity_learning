using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    
	public static CameraController instance;
    public Transform target;

    public Transform farBackground, middleBackground;

    public float minHeight, maxHeight;


    //private float lastXPos;
    private Vector2 lastPos;
    

    public float parallaxAmount;

    public bool stopFollow;

    private void Awake()
    {
    	instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //lastXPos = transform.position.x;
        lastPos = transform.position; //says it is equal to x and y of transform
    }

    // Update is called once per frame
    void Update()
    {
      
   		if(!stopFollow)
   		{


	   		transform.position = new Vector3(target.position.x, Mathf.Clamp(target.position.y, minHeight, maxHeight), transform.position.z);

	        //float amountToMoveX = transform.position.x - lastXPos;
	        Vector2 amountToMove = new Vector2(transform.position.x - lastPos.x, transform.position.y - lastPos.y);

	        farBackground.position = farBackground.position + new Vector3(amountToMove.x,amountToMove.y,0f);
	        middleBackground.position += new Vector3(amountToMove.x,amountToMove.y,0f) * parallaxAmount;

	       

	        //lastxPos = transform.position.x
	        lastPos = transform.position;
 	   }


    }
}
