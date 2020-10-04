using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryController : MonoBehaviour
{
    
	public GameObject[] dialogueBoxes;
	public float coolDownTime;
	private float coolDownRemaining;
	private int incrementer;

    // Start is called before the first frame update
    void Start()
    {
        coolDownRemaining = coolDownTime;
        incrementer = 1;
        dialogueBoxes[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        coolDownRemaining -= Time.deltaTime;
        if(coolDownRemaining <= 0f)
		{
			if(Input.GetButtonDown("Fire1"))
			{
				
				if(incrementer > 4)
				{
					incrementer = 4;
				}

				if(incrementer > 0)
				{
					dialogueBoxes[incrementer-1].SetActive(false);
					dialogueBoxes[incrementer].SetActive(true);
				} else
				{
					dialogueBoxes[incrementer].SetActive(true);
				}
				incrementer += 1;
			}
        }

        if(incrementer == 5)
        {
        	CameraVicController.instance.canMove = true;
        }
    }
}
