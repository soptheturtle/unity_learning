using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    
	public bool isGem, isHeal;
	public int healAmount;

	private bool isCollected;

	public GameObject pickupEffect;


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
    	if(other.CompareTag("Player") && !isCollected)
    	{
    		if(isGem)
    		{
    			LevelManager.instance.gemsCollected += 1;

    			isCollected = true;
    			Destroy(gameObject);
    			AudioManager.instance.PlaySFX(6);
    			Instantiate(pickupEffect, transform.position, transform.rotation);

    			UIController.instance.UpdateGemCount();
    		}

    		if(isHeal && PlayerHealthController.instance.currentHealth < PlayerHealthController.instance.maxHealth)
    		{
    			PlayerHealthController.instance.HealPlayer(healAmount);
    			AudioManager.instance.PlaySFX(7);
    			isCollected = true;
    			Destroy(gameObject);

    			Instantiate(pickupEffect, transform.position, transform.rotation);
    		}
    	}
    }
}
