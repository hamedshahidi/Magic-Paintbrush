using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour {

	public void NewGameBtn (string level1)
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

	}
	public void Quit (){
		Debug.Log ("Quit");
		Application.Quit ();
	}
	void Update () {
		if (Input.GetKey (KeyCode.Z)) {

			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
		} 
	}
}