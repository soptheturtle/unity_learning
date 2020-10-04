using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockController : MonoBehaviour
{
    
	private SpriteRenderer theSR;
	public Sprite[] whatIteration;

    // Start is called before the first frame update
    void Start()
    {
        theSR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float quickFix = PlayerController.instance.keyToUse;
        int useThisSprite = (int)quickFix;
        theSR.sprite = whatIteration[useThisSprite-1];
    }
}
