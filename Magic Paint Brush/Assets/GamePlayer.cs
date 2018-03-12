using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GamePlayer : MonoBehaviour {

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
	//GameMaster gm;

	[SerializeField]
	private GameObject bubbleprefab;

	public int lives;
	private int garbageremain;
	ViewController viewController;
	Conversation conversationscript;
	public GameObject LiangDailogbox;
	public Text LiangText;
	float horizontal;
	public GameObject drawingColliders;
	private bool fly;
	GameMaster gm;



	//AudioScript audioscriptjump;
	//AudioScript audioscriptcollectcoin;






	// Use this for initialization
	void Start () {
		
		
		garbageremain = 5;
		facingRight = true;
		fly = false;

		LiangText.text = "Oh God!! So much Garbages. Let, Clean the beach.\n\nPress   ➡  Key";
		myRigidbody = GetComponent<Rigidbody2D>();
		myAnimator = GetComponent<Animator> ();
		conversationscript=GetComponent<Conversation> ();
	
		lives = 5;
		viewController=GetComponent<ViewController>();
		gm = GameObject.Find("GameManager").GetComponent<GameMaster> ();


		//audioscriptjump =GameObject.Find("AudioObject").GetComponent<AudioScript> ();
		//audioscriptcollectcoin =GameObject.Find("AudioObject").GetComponent<AudioScript>();



	}


	// Update is called once per frame
	void FixedUpdate () {
		horizontal = Input.GetAxis ("Horizontal");
		HandleInput ();
		isGrounded = IsGrounded ();

		HandleMovement(horizontal);
		Flip (horizontal);


		HandleLayers ();
		myAnimator.SetBool ("land", false);
		ResetValues ();

		if (SceneManager.GetActiveScene ().name.Equals("Level 1")) {
			LiangText.text = "WHERE AM I!!!";
		}

	}
	private void HandleMovement(float horizontal){
		if (!myAnimator.GetBool ("slide") && (isGrounded || airControl)) {
			myRigidbody.velocity = new Vector2 (horizontal*movementSpeed, myRigidbody.velocity.y);  //x=-1,y=0
			myAnimator.SetFloat("speed",Mathf.Abs(horizontal));
		}


		if (IsGrounded() && jump) {
			isGrounded = false;
			myRigidbody.AddForce (new Vector2(0,430));
			myAnimator.SetTrigger ("jump");
			myAnimator.SetBool ("land", true);

			AudioScript.PlaySound ("jump");

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
			fly = true;
		}
		if(Input.GetKeyDown(KeyCode.Space)){
			myAnimator.SetTrigger ("jump");
			jump = true;

		}
		if (Input.GetKeyDown (KeyCode.LeftShift)) {
			myAnimator.SetTrigger ("throw");
			AudioScript.PlaySound ("bubbling");
			Throwbubble (0);
		}
	}

	private void Flip(float horizontal){

		if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight) {
			facingRight = !facingRight;
			Vector3 theScale = transform.localScale;
			theScale.x *= -1;
			transform.localScale = theScale;

			LiangText.rectTransform.localScale = new Vector3(LiangText.rectTransform.localScale.x*-1,LiangText.rectTransform.localScale.y,LiangText.rectTransform.localScale.z);

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
						myAnimator.SetBool ("land", false);
					

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
	public void Throwbubble(int value){
		if (!isGrounded && value == 1 || isGrounded && value == 0) {
			if (facingRight) {
				GameObject tmp=(GameObject)Instantiate (bubbleprefab, transform.position, Quaternion.identity);
				tmp.GetComponent<shootBubble> ().Initialize (Vector2.right);
			} else {
				GameObject tmp=(GameObject)Instantiate (bubbleprefab, transform.position, Quaternion.identity);
				tmp.GetComponent<shootBubble> ().Initialize (Vector2.left);
			}
		}

	}
	void OnTriggerEnter2D(Collider2D other){
		
			if (other.tag == "Brush") {
				StartCoroutine(MoreInfo());
				Destroy (other.gameObject);
				AudioScript.PlaySound ("coin");
			drawingColliders.SetActive (true);

			}
			if (other.tag == "garbages") {

				Destroy (other.gameObject);
			garbageremain = garbageremain - 1;
			AudioScript.PlaySound ("coin");
			if (garbageremain == 0) {
				viewController.HandleView ("trashbin");
				}
			}
		if (other.tag == "trashbin") {
			Destroy (other.gameObject);
			StartCoroutine(StartConversation());
			AudioScript.PlaySound ("coin");

		}
		if (other.tag == "Paint") {
			if (fly) {
				
				viewController.HandleView ("Drawing");
				AudioScript.PlaySound ("jump");
				fly = false;
			}
		}
		if (other.tag == "coin") {
			gm.coinCollected ();
			AudioScript.PlaySound ("coin");
			Destroy (other.gameObject);
		}
		if (other.tag == "life") {

			AudioScript.PlaySound ("jump");
			Destroy (other.gameObject);
		}
		if (other.tag == "endLevel") {

			StartCoroutine (coolDown());

		}
		if (other.tag == "die") {

			myAnimator.SetBool ("die", true);
			lives--;

			if (lives > 0) {

				StartCoroutine (LateCall());

			} else {
				myRigidbody.MovePosition (new Vector2(10,6));
			}
			AudioScript.PlaySound ("jump");
			//Destroy (other.gameObject);
		}
			

		

	}
	IEnumerator StartConversation()
	{
		yield return new WaitForSeconds (2);
		viewController.HandleView ("Mermaid");
		conversationscript.ShowConversation ();


	}
	IEnumerator MoreInfo()
	{
		yield return new WaitForSeconds (2);
		conversationscript.ShowMoreInfo();


	}


	IEnumerator LateCall()
	{	myAnimator.SetBool ("die", false);
		yield return new WaitForSeconds (2);

		myAnimator.SetBool ("idle", true);

		myRigidbody.MovePosition (new Vector2(10,6));


	}
	IEnumerator coolDown()
	{
		yield return new WaitForSeconds (2);
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);



	}




}
