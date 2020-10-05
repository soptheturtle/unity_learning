using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenManager : MonoBehaviour
{
    
	public float waitToLoad;
	public string instructionsLevel;

    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(800, 800, true);
    }

    // Update is called once per frame
    void Update()
    {
        
        waitToLoad -= Time.deltaTime;
        if(waitToLoad < 0f && Input.GetButtonDown("Fire1"))
        {
        	SceneManager.LoadScene(instructionsLevel);
        }

        if(waitToLoad < 0f && Input.GetButtonDown("Fire2"))
        {
        	Application.Quit();
        }
    }
}
