using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectorController : MonoBehaviour
{
    

	public int pointToAdd;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
    	

    	if(other.gameObject.tag == "Connector")
    	{
    		lightPlayerController.instance.playerPoints += pointToAdd;
    		lightPlayerController.instance.CreateGoodTime();
    	}
    }

}
