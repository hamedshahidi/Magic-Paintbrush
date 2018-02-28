using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class ButtomManager : MonoBehaviour {

	public void NewGameBtn (string MagicPaintBrush)
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

	}
	public void Quit(){
		Debug.Log ("Quit");
		Application.Quit ();
	}

}