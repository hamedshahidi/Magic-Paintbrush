using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IgnoreCollision : MonoBehaviour {
	


	[SerializeField]
	private Collider2D other;

	private void Awake () 
	{
		Physics2D.IgnoreCollision(GetComponent<Collider2D>(), other, true);
		
	}

}
