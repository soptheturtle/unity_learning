﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class instructionController : MonoBehaviour
{
    

	public string levelToLoad;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.DeleteAll();
    }

    // Update is called once per frame
    void Update()
    {
        
    	if(Input.GetButtonDown("Fire1"))
    	{
    	SceneManager.LoadScene(levelToLoad);
    	}


    }
}
