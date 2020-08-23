using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class partyCameraController : MonoBehaviour
{
    
	public Transform target;
	public float minHorizontal,maxHorizontal,minHeight,maxHeight;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     transform.position = new Vector3(Mathf.Clamp(target.position.x, minHorizontal,maxHorizontal), Mathf.Clamp(target.position.y+1f, minHeight, maxHeight), transform.position.z);   
    }
}
