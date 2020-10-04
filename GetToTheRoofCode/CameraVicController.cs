using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraVicController : MonoBehaviour
{
    
	public static CameraVicController instance;

	public float cameraSpeed;
	public Transform targetLocation;
	public bool canMove;
	public GameObject uiMessage;

	public float messageCountdown;
	private float currentCountdown;

	void Awake()
	{
		instance = this;
	}

    // Start is called before the first frame update
    void Start()
    {
        canMove = false;
        currentCountdown = messageCountdown;
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove)
        {
        	transform.position = Vector3.MoveTowards(transform.position, targetLocation.position, cameraSpeed*Time.deltaTime);
        }

        if(Vector3.Distance(transform.position, targetLocation.position) <= 0.25f)
        {
        	//Activating stuff here
        	uiMessage.SetActive(true);
        	currentCountdown -= Time.deltaTime;
        }

        if(currentCountdown <= 0f && Input.GetButtonDown("Fire1"))
        {
        	Application.Quit();
        }
    }
}
