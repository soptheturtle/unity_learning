using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InstructionsManager : MonoBehaviour
{
	public float waitToLoad;
	public string firstLevel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        waitToLoad -= Time.deltaTime;
        if(waitToLoad < 0f && Input.GetButtonDown("Fire1"))
        {
        	SceneManager.LoadScene(firstLevel);
        }
    }
}
