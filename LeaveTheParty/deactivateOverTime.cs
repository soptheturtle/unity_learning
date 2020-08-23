using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deactivateOverTime : MonoBehaviour
{
    

	public float deactivateTime = 1f;
	private float currentTime;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = deactivateTime;
    }

    // Update is called once per frame
    void Update()
    {
      
      currentTime -= Time.deltaTime;
      if(currentTime < 0)
      {
      	currentTime = deactivateTime;
      	gameObject.SetActive(false);
      }   

    }
}
