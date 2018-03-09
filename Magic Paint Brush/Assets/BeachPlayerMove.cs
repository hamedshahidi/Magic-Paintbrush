using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeachPlayerMove : MonoBehaviour {
	public static Animator playeranimatorinbeach;
	public static Rigidbody2D playerbody;



	// Use this for initialization

	void Start () {
		playeranimatorinbeach=GetComponent<Animator> ();



	}
	// Update is called once per frame
	public static void doSomething (string action) {
		switch (action) {
		case "move":
			playeranimatorinbeach.SetTrigger ("move");
			break;
		case "idle":
			playeranimatorinbeach.SetTrigger ("idle");
			break;
	

		}

	}


}
