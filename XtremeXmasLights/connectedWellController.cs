using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class connectedWellController : MonoBehaviour
{
    
	public float aliveTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(aliveTime <= 0.05f)
        {
        	Destroy(gameObject);
        }

        aliveTime -= Time.deltaTime;
    }
}
