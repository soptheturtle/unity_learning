using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class lightVictoryController : MonoBehaviour
{
    
    public float[] transition;
    public int threshold0;
    public int threshold1;
    public int threshold2;
    public int threshold3;
    private bool creditsDone;

    [Header("End 0")]
    public GameObject end00;
    public GameObject end01;
    public GameObject end02;
    public GameObject end03;
    public GameObject end04;

    [Header("End 1")]
    public GameObject end10;
    public GameObject end11;
    public GameObject end12;
    public GameObject end13;

    [Header("End 2")]
    public GameObject end20;
    public GameObject end21;
    public GameObject end22;
    public GameObject end23;

    [Header("End 3")]
    public GameObject end30;
    public GameObject end31;
    public GameObject end32;
    public GameObject end33;
    public GameObject roofer;





    private int playerScore;

    // Start is called before the first frame update
    void Start()
    {
        creditsDone = false;
        playerScore = PlayerPrefs.GetInt("PlayerPoints");
        if(playerScore < threshold0)
        {
        	Ending0();
        } else if(playerScore < threshold1) 
        {
        	Ending1();
        } else if(playerScore < threshold2) 
        {
        	Ending2();
        } else if(playerScore >= threshold2)
        {
        	Ending3();
        } else
        {
        	Debug.Log("CRITICAL ERROR! Invalid Player Score!");
        }
        lightAudioManager.instance.StartEndMusic();

    }

    // Update is called once per frame
    void Update()
    {
        
    	if(creditsDone)
    	{
    		if(Input.GetButtonDown("Fire1"))
    		{
    			Debug.Log("Quitting the game.");
    			Application.Quit();
    		}
    	}

    }



    public void Ending0()
    {
    StartCoroutine(Victory0());
    }

    public void Ending1()
    {
    StartCoroutine(Victory1());
    }

    public void Ending2()
    {
    StartCoroutine(Victory2());
    }

    public void Ending3()
    {
    StartCoroutine(Victory3());
    }




    public IEnumerator Victory0()
    {
    	
    	end00.SetActive(true);
    	yield return new WaitForSeconds(transition[1]);
    	end01.SetActive(true);
    	end00.SetActive(false);
    	yield return new WaitForSeconds(transition[2]);
    	end02.SetActive(true);
    	end01.SetActive(false);
    	yield return new WaitForSeconds(transition[3]);
    	end03.SetActive(true);
    	end02.SetActive(false);
    	yield return new WaitForSeconds(transition[2]);
    	end04.SetActive(true);
    	end03.SetActive(false);
    	yield return new WaitForSeconds(transition[0]);
    	creditsDone = true; 	
	}

    public IEnumerator Victory1()
    {
    	
    	end10.SetActive(true);
    	yield return new WaitForSeconds(transition[3]);
    	end11.SetActive(true);
    	end10.SetActive(false);
    	yield return new WaitForSeconds(transition[3]);
    	end12.SetActive(true);
    	end11.SetActive(false);
    	yield return new WaitForSeconds(transition[2]);
    	end13.SetActive(true);
    	end12.SetActive(false);
    	yield return new WaitForSeconds(transition[0]);
    	creditsDone = true; 	
	}

    public IEnumerator Victory2()
    {
    	
    	end20.SetActive(true);
    	yield return new WaitForSeconds(transition[3]);
    	end21.SetActive(true);
    	end20.SetActive(false);
    	yield return new WaitForSeconds(transition[3]);
    	end22.SetActive(true);
    	end21.SetActive(false);
    	yield return new WaitForSeconds(transition[2]);
    	end23.SetActive(true);
    	end22.SetActive(false);
    	yield return new WaitForSeconds(transition[0]);
    	creditsDone = true; 	
	}

    public IEnumerator Victory3()
    {
    	
    	end30.SetActive(true);
    	yield return new WaitForSeconds(transition[3]);
    	end31.SetActive(true);
    	end30.SetActive(false);
    	roofer.SetActive(true);
    	yield return new WaitForSeconds(transition[3]);
    	yield return new WaitForSeconds(transition[1]);
    	end32.SetActive(true);
    	roofer.SetActive(false);
    	end31.SetActive(false);
    	yield return new WaitForSeconds(transition[2]);
    	end33.SetActive(true);
    	end32.SetActive(false);
    	yield return new WaitForSeconds(transition[0]);
    	creditsDone = true; 	
	}


}
