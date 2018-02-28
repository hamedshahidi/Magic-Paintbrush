using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class ButtomManager : MonoBehaviour {

	public void NewGameBtn (string MagicPaintBrush)
	{
<<<<<<< HEAD
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

	}
	public void Quit(){
		Debug.Log ("Quit");
		Application.Quit ();
	}

=======
	SceneManager.LoadScene(MagicPaintBrush);

	}
>>>>>>> f4cd4ae8e52ee2b8986d99d05f8134a6be420af2
}