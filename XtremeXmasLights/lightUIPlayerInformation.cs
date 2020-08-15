using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lightUIPlayerInformation : MonoBehaviour
{
    
    public static lightUIPlayerInformation instance;
    
    public Sprite[] zeroToNine;

    public Image playerPointsH, playerPointsT, playerPointsO;
    public Image playerPiecesH, playerPiecesT, playerPiecesO;

    public int pointH, pointT, pointO;
    public int pieceH, pieceT, pieceO;

	void Awake()
	{
		instance =  this;
	}
    // Start is called before the first frame update
    void Start()
    {
        // pointH = Mathf.FloorToInt(lightPlayerController.instance.playerPoints/100f);
        // pointT = Mathf.FloorToInt(Mathf.FloorToInt(lightPlayerController.instance.playerPoints*1f/100f - pointH)*10);
        // pointO = Mathf.FloorToInt(8);
        // pieceH = Mathf.FloorToInt(2);
        // pieceT = Mathf.FloorToInt(7);
        // pieceO = Mathf.FloorToInt(6);

    }

    // Update is called once per frame
    void Update()
    {
        

        pointH = Mathf.FloorToInt(lightPlayerController.instance.playerPoints/100f);
        if(lightPlayerController.instance.playerPoints > 100)
        {
    		pointT = Mathf.FloorToInt(lightPlayerController.instance.playerPoints/100f*10f) - 10 * pointH;
    		pointO = Mathf.FloorToInt((lightPlayerController.instance.playerPoints/10f - pointT) * 10f) - 100 * pointH;
    	}
    	else
    	{
    		pointT = Mathf.FloorToInt(lightPlayerController.instance.playerPoints/100f*10f);
    	    pointO = Mathf.FloorToInt((lightPlayerController.instance.playerPoints/10f - pointT) * 10f);
    	}

        pieceH = Mathf.FloorToInt(lightPlayerController.instance.pieceLimit/100f);
        if(lightPlayerController.instance.pieceLimit > 100)
        {
        	pieceT = Mathf.FloorToInt(lightPlayerController.instance.pieceLimit/100f*10f) - 10;
        	pieceO = Mathf.FloorToInt((lightPlayerController.instance.pieceLimit/10f - pieceT) * 10f) - 100;
    	} else
    	{
    		pieceT = Mathf.FloorToInt(lightPlayerController.instance.pieceLimit/100f*10f);
    		pieceO = Mathf.FloorToInt((lightPlayerController.instance.pieceLimit/10f - pieceT) * 10f);
    	}
        

        if(lightPlayerController.instance.pieceLimit == 100 || lightPlayerController.instance.pieceLimit == 200)
        {
        	pieceT = 0;
        }
        if(lightPlayerController.instance.playerPoints == 100 || lightPlayerController.instance.playerPoints == 200 || lightPlayerController.instance.playerPoints == 300 || lightPlayerController.instance.playerPoints == 400 || lightPlayerController.instance.playerPoints == 500)
        {
        	pointT = 0;
        }
        
         playerPointsH.sprite = zeroToNine[pointH];
         playerPointsT.sprite = zeroToNine[pointT];
         playerPointsO.sprite = zeroToNine[pointO];

         playerPiecesH.sprite = zeroToNine[pieceH];
         playerPiecesT.sprite = zeroToNine[pieceT];
         playerPiecesO.sprite = zeroToNine[pieceO];

    }
}
