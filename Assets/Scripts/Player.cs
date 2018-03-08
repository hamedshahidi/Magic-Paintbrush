using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
	
	private Player instance;
	public Player Instance{
		get{
			if(instance==null)
			return instance;
		}
	}

	private Animator myAnimator;

	[SerializeField]
	private float movementSpeed;
	private bool facingRight;


	[SerializeField]
	private Transform[] groundPoints;

	[SerializeField]
	private float groundRadius;

	[SerializeField]
	private LayerMask whatIsGround;
	[SerializeField]
	private bool airControl;



	[SerializeField]
	private float jumpForce;

	GameMaster gm;

	public int lives;

	private Rigidbody2D MyRigidbody{ get; set;}

	public bool Attack{get;set;}

	public bool Slide{get;set;}

	public bool Jump{get;set;}

	public bool OnGround{get;set;}

	// Use this for initialization
	void Start () {
		facingRight = true;

		MyRigidbody = GetComponent<Rigidbody2D>();
		myAnimator = GetComponent<Animator> ();

		gm = GameObject.Find("GameManager").GetComponent<GameMaster> ();
		lives = 5;
		//audioscriptjump =GameObject.Find("AudioObject").GetComponent<AudioScript> ();
		//audioscriptcollectcoin =GameObject.Find("AudioObject").GetComponent<AudioScript>();


		
	}
	void Update(){
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float horizontal = Input.GetAxis ("Horizontal");
		OnGround = IsGrounded ();
		HandleInput ();
		HandleMovement(horizontal);
		Flip (horizontal);


		HandleLayers ();


		
	}
	private void HandleMovement(float horizontal){
		if (MyRigidbody.velocity.y < 0) {
			myAnimator.SetBool ("land",true);
		}
		if (!Attack && !Slide && (OnGround || airControl)) {
			//MyRigidbody.velocity = new Vector2 (horizontal * movementSpeed, myAnimator.velocity.y);
		}
		if (Jump && MyRigidbody.velocity.y==0) {
			MyRigidbody.AddForce (new Vector2 (0, jumpForce));
		}
		myAnimator.SetFloat ("speed", Mathf.Abs (horizontal));
	}
	private void HandleInput(){
		if (Input.GetKeyDown (KeyCode.LeftControl)) {
			
		}
		if(Input.GetKeyDown(KeyCode.Space)){
			

		}
		if (Input.GetKeyDown (KeyCode.LeftShift)) {
			myAnimator.SetTrigger ("throw");
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


	private bool IsGrounded(){
		if (MyRigidbody.velocity.y <= 0) {
			foreach(Transform point in groundPoints){
				Collider2D[] colliders = Physics2D.OverlapCircleAll (point.position, groundRadius, whatIsGround);
				for (int i = 0; i < colliders.Length; i++) {
					if (colliders [i].gameObject != gameObject) {
						

						return true;
					}
				}
			}
		}
		return false;
	}
	private void HandleLayers(){
		if (!OnGround) {
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
			
			AudioScript.PlaySound ("jump");
			Destroy (other.gameObject);
		}

		if (other.tag == "die" || other.tag == "Enemies") {
			
			myAnimator.SetBool ("die", true);
			lives--;

			if (lives > 0) {
				
				StartCoroutine (LateCall());

				} else {
				
			}
			AudioScript.PlaySound ("jump");
			//Destroy (other.gameObject);
		}

	}


	IEnumerator LateCall()
	{
		yield return new WaitForSeconds (2);
		myAnimator.SetBool ("die", false);

		MyRigidbody.MovePosition (new Vector2(0,0));


	}





}
