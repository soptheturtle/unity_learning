using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class barUIController : MonoBehaviour
{
    
    public static barUIController instance;
    private int restPoints, karmaPoints;
    public RectTransform restBar, karmaBar;

    public Image fadeScreen;
    public float fadeSpeed;
    public bool startFading;

    void Awake()
    {
    	instance =  this;
    }

    // Start is called before the first frame update
    void Start()
    {
     fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, 0f);
     startFading = false;
    }

    // Update is called once per frame
    void Update()
    {
        restPoints = playerController.instance.restPoints;
        karmaPoints = playerController.instance.karmaPoints;

   		restBar.sizeDelta = new Vector2(9, restPoints);
   		restBar.anchoredPosition = new Vector2(restBar.anchoredPosition.x, restPoints);

   		karmaBar.sizeDelta = new Vector2(9, karmaPoints);
   		karmaBar.anchoredPosition = new Vector2(karmaBar.anchoredPosition.x, karmaPoints);
        
       
       if(startFading)
       {
       		fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
   		}
  
        


    }
}
