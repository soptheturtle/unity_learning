using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugSprayContextController : MonoBehaviour
{
    

	public float bugSprayCooldown;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(bugSprayCooldown <= 0f)
        {
        	gameObject.SetActive(false);
        }

        bugSprayCooldown -= Time.deltaTime;
    }
}
