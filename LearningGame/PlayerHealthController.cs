using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;
	
	public int currentHealth, maxHealth;

	public float invincibleLength;
	private float invincibleCounter;
	private SpriteRenderer theSR;

	public GameObject deathEffect;

	//Called just before start function
	private void Awake()
	{
		instance = this;
	}


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        theSR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(invincibleCounter > 0) 
        {
        	invincibleCounter -= Time.deltaTime; //counts down in seconds based on # of frames you're running for        	
        	if(invincibleCounter <= 0)
        	{
        		theSR.color = new Color(theSR.color.r, theSR.color.g, theSR.color.b, 1.0f);
        	}

        }
    }

    public void DealDamage(int other) 
    {
    	//currentHealth = currentHealth - 1;
    	//currentHealth--; //-- means take away one. so ++ means add one
    	if(invincibleCounter <= 0) 
    	{
    		currentHealth -= other;
    	
    	

    	if(currentHealth <= 0)
    	{
    		//gameObject.SetActive(false); Moving to have the level manger deal with this
    		Instantiate(deathEffect, transform.position, transform.rotation);
    		LevelManager.instance.RespawnPlayer();
    		

    	} else {
    		invincibleCounter = invincibleLength;
    		theSR.color = new Color(theSR.color.r, theSR.color.g, theSR.color.b, 0.5f);

    		PlayerController.instance.KnockBack();
    		AudioManager.instance.PlaySFX(9);
    		
    	}

    	UIController.instance.UpdateHealthDisplay();
    }

    }

    public void HealPlayer(int other)
    {
    	currentHealth += other;
    	if(currentHealth > maxHealth)
    	{
    		currentHealth = maxHealth;
    	}

    	UIController.instance.UpdateHealthDisplay();

    }


}
