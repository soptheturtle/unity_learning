using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelManager : MonoBehaviour
{
    
	public string levelToLoad;
	
	public Transform thePlayer;

	public GameObject letsGo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    	if(Vector3.Distance(gameObject.transform.position, thePlayer.position) <= 3f)
    	{
    		letsGo.SetActive(true);
    	} else
    	{
    		letsGo.SetActive(false);
    	}

    	if(Vector3.Distance(gameObject.transform.position, thePlayer.position) <= 3f && Input.GetButtonDown("Fire1"))
    	{
        	PlayerPrefs.SetInt("totalPoints",playerController.instance.restPoints + playerController.instance.karmaPoints);
        	barUIController.instance.startFading = true;
        	LoadNextLevelCo();

    	}
    }

    private void LoadNextLevelCo()
    {
    	StartCoroutine(LoadNextLevel());
    }

    private IEnumerator LoadNextLevel()
    {
    	yield return new WaitForSeconds(2.0f);
    	SceneManager.LoadScene(levelToLoad);

    }
}
