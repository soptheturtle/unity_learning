using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    
    public static ChestController instance;

    public Transform thePlayer;
    public GameObject openChestDialogue;
    public GameObject failedToOpen;
    public GameObject gotBugSpray;
    public float distanceToOpen;
    public bool chestAttempted;

    void Awake()
    {
    	instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        chestAttempted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, thePlayer.position) <= distanceToOpen && PlayerController.instance.selectedKey > 0 && !chestAttempted)
        {
        	openChestDialogue.SetActive(true);
        	if(Input.GetButtonDown("Fire1"))
        	{
        		openChestDialogue.SetActive(false);
        		if(PlayerController.instance.selectedKey == PlayerController.instance.keyToUse)
        		{
        			gotBugSpray.SetActive(true);
        			PlayerController.instance.hasBugSpray = true;
        			chestAttempted = true;
        			
        		} else 
        		{
        			failedToOpen.SetActive(true);
        			chestAttempted = true;
        		}
        	}

        } else if(Vector3.Distance(transform.position, thePlayer.position)  > distanceToOpen)
        {
        	openChestDialogue.SetActive(false);
        }
    }
}
