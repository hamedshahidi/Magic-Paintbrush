using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
	public GameObject pausedMenu;



	
	void Start(){
		
		
	}
	void Update(){
		
		if (Input.GetKeyDown (KeyCode.Escape)) {
			
				pausedMenu.SetActive (true);





		}
	}

	public void PlayGame(){
		//Conversationscript.handleConversation ();
		StartCoroutine (coolDown());

		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
		
		
	}

	public void QuitGame(){
		Debug.Log ("Quit");
		Application.Quit ();

	}


     public void Continue()
    {
		

    }

	IEnumerator coolDown()
	{
		
		yield return new WaitForSeconds (2);




	}



}
