using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/// <summary>
/// Ignore collision.
/// </summary>
public class IgnoreCollision : MonoBehaviour {
	

	/// <summary>
	/// The other.
	/// filed is serialize so it can be accessed from the unity terminal
	/// Collider2D (other) provides interface in the unity terminal to put the player so it recognizes player when on collision 
	/// </summary>
	[SerializeField]
	private Collider2D other;
	/// <summary>
	/// Awake this instance.
	/// when awake this function helps the enemy ignore collision when it collides with the player
	/// </summary>
	private void Awake () 
	{
		Physics2D.IgnoreCollision(GetComponent<Collider2D>(), other, true);
		
	}

}
