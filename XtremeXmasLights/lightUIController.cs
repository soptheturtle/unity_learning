using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lightUIController : MonoBehaviour
{
    

    public static lightUIController instance;

    public int pieceChosen = 1;

	void Awake()
	{
		instance= this;
		pieceChosen = 1;
	}

    // Start is called before the first frame update
    void Start()
    {
    	pieceChosen = 1;
    }

    // Update is called once per frame
    void Update()
    {
        

        if(Input.GetButtonDown("Jump"))
        {
        	if(pieceChosen == 7)
        	{
        		pieceChosen = 1;
        	}
        	else
        	{
        		pieceChosen += 1;
        	}
        }

    }
}
