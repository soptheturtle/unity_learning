using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillboxController : MonoBehaviour
{
    
	public static KillboxController instance;
	public Transform killBoxPos;
	public float killBoxOffset;


	private void Awake()
	{
		instance = this;
	}

    // Start is called before the first frame update
    void Start()
    {
       killBoxPos = GetComponent<Transform>();
       killBoxPos.position = new Vector3(PlayerController.instance.transform.position.x,PlayerController.instance.transform.position.y - killBoxOffset,0f);

    }

    // Update is called once per frame
    void Update()
    {
        killBoxPos.position = new Vector3(PlayerController.instance.transform.position.x,killBoxPos.position.y,0f);   
    }

        void OnTriggerEnter2D(Collider2D other) 
   		{
    	if(other.tag == "Player") 
    	{
    		//Debug.Log("Hit");

    		//FindObjectOfType<PlayerHealthController>().DealDamage();

    		PlayerHealthController.instance.DealDamage(PlayerHealthController.instance.maxHealth);
    	}
  	
    }
}

