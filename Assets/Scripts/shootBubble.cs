using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootBubble : MonoBehaviour {
	public GameObject projectile;
	public Vector2 velocity;
	public bool canShoot=true;
	public Vector2 offset = new Vector2 (0.4f,0.1f);
	public float coolDown=1f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.LeftShift) && canShoot) {
			GameObject go=Instantiate (projectile,(Vector2) transform.position + offset * transform.localScale.x, Quaternion.identity);
			go.GetComponent<Rigidbody2D> ().velocity = new Vector2 (velocity.x*transform.localScale.x+4,velocity.y);
			AudioScript.PlaySound ("bubbling");
			StartCoroutine (CanShoot());
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "coin") {
			
			Destroy (other.gameObject);
		}


	}

	IEnumerator CanShoot()
	{
		canShoot = false;
		yield return new WaitForSeconds (1);
		canShoot = true;


	}
}
