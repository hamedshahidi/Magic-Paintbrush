using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewController : MonoBehaviour {
	
	public GameObject Drawing;
	public GameObject MonsterView;

	void Start(){
	
	}

	public void HandleView(string View){
		
		switch (View) {

		case "Drawing":
			
			Drawing.SetActive (true);
			break;
		case "MonsterView":
			Drawing.SetActive (false);
			MonsterView.SetActive (true);
			break;


		}
	}

}
