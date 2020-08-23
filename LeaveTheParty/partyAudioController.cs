using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class partyAudioController : MonoBehaviour
{
    
	public static partyAudioController instance;
	public AudioSource[] soundEffects;

	public AudioSource bgm, goodEndMusic, badEndMusic;

	public float walkingCooldown;
	private float walkingCurrent;

	void Awake()
	{
		instance = this;
	}

    // Start is called before the first frame update
    void Start()
    {
        if(SceneManager.GetActiveScene().name == "mainTitle" || SceneManager.GetActiveScene().name == "SampleScene" || SceneManager.GetActiveScene().name == "gameInstructions" || SceneManager.GetActiveScene().name == "finalScene")
        {
        	bgm.Play();
        } else if(SceneManager.GetActiveScene().name == "creditsSequence" && PlayerPrefs.GetInt("totalPoints") >= 40)
        {
        	goodEndMusic.Play();
        } else if(SceneManager.GetActiveScene().name == "creditsSequence")
        {
        	badEndMusic.Play();
        }

        walkingCurrent = walkingCooldown;

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void PlaySFX(int soundToPlay)
    {
    	soundEffects[soundToPlay].Stop();
    	soundEffects[soundToPlay].Play();
    }

    public void PlaySFXAlert(int soundToPlay)
    {
    	soundEffects[2].Play();
    }

    public void PlaySFXWalking()
    {
    	
    	
    	if(walkingCurrent <= 0)
    	{
    	soundEffects[3].Play();
    	walkingCurrent = walkingCooldown;
    	} else
    	{
    		walkingCurrent -= Time.deltaTime;
    	}

    }

        public void StopSFXWalking()
    {
    	
    	soundEffects[3].Stop();
    }



}
