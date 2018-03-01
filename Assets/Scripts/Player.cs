﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
	private Rigidbody2D myRigidbody;
	private Animator myAnimator;

	[SerializeField]
	private float movementSpeed;
	private bool facingRight;
	private bool slide;

	[SerializeField]
	private Transform[] groundPoints;

	[SerializeField]
	private float groundRadius;

	[SerializeField]
	private LayerMask whatIsGround;
	private bool isGrounded;
	private bool jump;
	private bool land;
	[SerializeField]
	private float jumpForce;
	[SerializeField]
	private bool airControl;
	GameMaster gm;
	//AudioScript audioscriptjump;
	//AudioScript audioscriptcollectcoin;






	// Use this for initialization
	void Start () {
		facingRight = true;
		myRigidbody = GetComponent<Rigidbody2D>();
		myAnimator = GetComponent<Animator> ();
		gm = GameObject.Find("GameManager").GetComponent<GameMaster> ();
		//audioscriptjump =GameObject.Find("AudioObject").GetComponent<AudioScript> ();
		//audioscriptcollectcoin =GameObject.Find("AudioObject").GetComponent<AudioScript>();


		
	}
	void Update(){
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float horizontal = Input.GetAxis ("Horizontal");
		isGrounded = IsGrounded ();
		HandleInput ();
		HandleMovement(horizontal);
		Flip (horizontal);


		HandleLayers ();

		ResetValues ();

		
	}
	private void HandleMovement(float horizontal){
		if (!myAnimator.GetBool ("slide") && (isGrounded || airControl)) {
			myRigidbody.velocity = new Vector2 (horizontal*movementSpeed, myRigidbody.velocity.y);  //x=-1,y=0
			myAnimator.SetFloat("speed",Mathf.Abs(horizontal));
		}


		if (isGrounded && jump) {
			isGrounded = false;
			myRigidbody.AddForce (new Vector2(0,jumpForce));
			myAnimator.SetTrigger ("jump");

			AudioScript.PlaySound ("jump");
			myAnimator.SetBool ("land",true);
		}
		if (slide && !this.myAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Slide")) {
			myAnimator.SetBool ("slide", true);
		}else if(!this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName ("Slide")){
			myAnimator.SetBool ("slide", false);
		}
	}
	private void HandleInput(){
		if (Input.GetKeyDown (KeyCode.LeftControl)) {
			slide = true;
		}
		if(Input.GetKeyDown(KeyCode.Space)){
			jump = true;


		}
	}

	private void Flip(float horizontal){
		
		if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight) {
			facingRight = !facingRight;
			Vector3 theScale = transform.localScale;
			theScale.x *= -1;
			transform.localScale = theScale;
		}
	}
	private void ResetValues(){
		slide = false;
		jump = false;
	}

	private bool IsGrounded(){
		if (myRigidbody.velocity.y <= 0) {
			foreach(Transform point in groundPoints){
				Collider2D[] colliders = Physics2D.OverlapCircleAll (point.position, groundRadius, whatIsGround);
				for (int i = 0; i < colliders.Length; i++) {
					if (colliders [i].gameObject != gameObject) {
						myAnimator.ResetTrigger ("jump");

						return true;
					}
				}
			}
		}
		return false;
	}
	private void HandleLayers(){
		if (!isGrounded) {
			myAnimator.SetLayerWeight (1, 1);
		}else{
			myAnimator.SetLayerWeight (1, 0);

			}
		
		}
	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "coin") {
			gm.coinCollected ();
			AudioScript.PlaySound ("coin");
			Destroy (other.gameObject);
		}
		if (other.tag == "life") {
			gm.coinCollected ();
			AudioScript.PlaySound ("jump");
			Destroy (other.gameObject);
		}
	}



}
