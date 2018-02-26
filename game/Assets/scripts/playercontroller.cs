using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercontroller : MonoBehaviour {


	public float maxSpeed;
	public float jumpHeight;

	bool facingRight;


	Rigidbody2D  myBody;
	Animator myAnim;
	// Use this for initialization
	void Start () {
		myBody = GetComponent<Rigidbody2D> ();
		myAnim = GetComponent<Animator> ();


		facingRight = true;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float move = Input.GetAxis ("Horizontal");

		myBody.velocity = new Vector2 (move*maxSpeed, myBody.velocity.y);

		myAnim.SetFloat ("speed", Mathf.Abs (move));
		if (move > 0 && !facingRight) {
			flip ();
		} else if (move < 0 && facingRight) {
			flip ();
		}
		
	}
	void flip(){
		facingRight = !facingRight;
		Vector3 thescale = transform.localScale;
		thescale.x *= -1;
		transform.localScale = thescale;
	}
}
