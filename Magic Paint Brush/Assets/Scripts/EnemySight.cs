using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemySight : MonoBehaviour {
	
	[SerializeField]
	private Enemy enemy;


	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player" )
		{
			enemy.Target = other.gameObject;
		}

	}
	void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			enemy.Target = null;
		}
	}
}
