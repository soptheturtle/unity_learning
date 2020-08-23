using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finalController : MonoBehaviour
{
    

	public float waitTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(waitTime >= 0)
        {
        	waitTime -= Time.deltaTime;
        }

        if(waitTime < 0)
        {
        	if(Input.GetButtonDown("Fire1"))
        	{
        		Application.Quit();
        	}
        }
    }
}
