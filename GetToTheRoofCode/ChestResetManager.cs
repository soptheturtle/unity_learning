using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestResetManager : MonoBehaviour
{
    

	public static ChestResetManager instance;

	void Awake()
	{
		instance = this;
	}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetChestPuzzle()
    {
    	if(!PlayerController.instance.hasBugSpray)
    	{
    	PlayerController.instance.selectedKey = 0;
    	ChestController.instance.chestAttempted = false;
    	KeyController.instance.startSelecting = false;
    	PlayerController.instance.canSelectKeys = false;
    	PlayerController.instance.selectingKeys = false;
    	ChestController.instance.failedToOpen.SetActive(false);
    	}
    }
}
