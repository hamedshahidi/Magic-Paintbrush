using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemySight : MonoBehaviour {
	
	[SerializeField]
	private Enemy enemy;

	/// <summary>
	/// Raises the trigger enter2 d event.
	/// when enemy collides with another objects. it sets the object as target
	/// </summary>
	/// <param name="other">Other.</param>
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player" )
		{
			enemy.Target = other.gameObject;
		}

	}
	/// <summary>
	/// Raises the trigger exit2 d event.
	/// makes the enemy able to exit collision from target
	/// </summary>
	/// <param name="other">Other.</param>
	void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			enemy.Target = null;
		}
	}
}
