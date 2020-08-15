using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lightUISelector : MonoBehaviour
{
    

	public Image selector;
	public Sprite selectorOn, selectorOff;
	public int spritePosition;
	



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if(spritePosition == lightUIController.instance.pieceChosen)
        {
        	selector.sprite = selectorOn;
        } else
        {
        	selector.sprite = selectorOff;
        }
    }
}
