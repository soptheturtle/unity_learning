using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class lightAudioManager : MonoBehaviour
{
    
	public static lightAudioManager instance;
	public AudioSource[] soundEffects;

	public AudioSource bgm, levelEndMusic, endMusic;


	void Awake()
	{
		instance = this;
	}

    // Start is called before the first frame update
    void Start()
    {
        
        if(SceneManager.GetActiveScene().name =="victory0Screen")
        {
        	Debug.Log("Doing nothing!");
        } else
        {
	       bgm.Play();
	    }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void PlaySFX(int soundToPlay)
    {
    	soundEffects[soundToPlay].Stop();

    	soundEffects[soundToPlay].pitch = Random.Range(0.9f, 1.1f);

    	soundEffects[soundToPlay].Play();
    }

    public void StopSFX(int soundToStop)
    {
    	soundEffects[soundToStop].Stop();
    }

    public void StartLevelEndMusic()
    {
    	bgm.Stop();
    	levelEndMusic.Play();

    }

    public void StartBGM()
    {
    	bgm.Play();
    }

    public void StartEndMusic()
    {
    	endMusic.Play();
    }

}
