using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightStomper : MonoBehaviour
{
    
	private int offsetNum; 
	public float amountToMove;

    // Start is called before the first frame update
    void Start()
    {
      offsetNum = this.transform.parent.GetComponent<XmasLightController>().spriteToRender;
      
      if(offsetNum >= 5)
      {
    	  transform.position = new Vector3(transform.position.x + amountToMove, transform.position.y, transform.position.z);
  	  }
    }

    // Update is called once per frame
    void Update()
    {




    }


        private void OnTriggerEnter2D(Collider2D other)
   	{
    	
    	if(other.gameObject.tag == "Crusher" && !gameObject.GetComponentInParent<XmasLightController>().isCrushed && lightPlayerController.instance.isFalling == false)
    	{
    		gameObject.GetComponentInParent<XmasLightController>().isCrushed = true;
    		if(lightPlayerController.instance.playerPoints -5 < 0)
    		{
    			lightPlayerController.instance.playerPoints = 0;
    		} else
    		{
    			lightPlayerController.instance.playerPoints -= 5;	
    		}
    		
    		StartCoroutine(lightPlayerController.instance.LightCrushed());

    	}
    }

}
