using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour {
	
	public int coin;
	public Text cointcounter;
	private int coinscoint;


	// Use this for initialization
	void Start () {
		
		
		
	}
	
	// Update is called once per frame
	void Update () {
		cointcounter.text = coin.ToString ();
		
	}

	public void coinCollected(){
		coin += 1;

	}
}
