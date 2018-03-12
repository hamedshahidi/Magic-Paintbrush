using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesScript : MonoBehaviour {
	public static bool flyTrigger;
	private Rigidbody2D EnemiesRigidBody;
	public GameObject showBubbleforFlying;
	public GameObject CoinObject;
	private float horizontal;
	private Animator EnemyAnimator;

	void Start(){
		flyTrigger = false;
		EnemiesRigidBody=GetComponent<Rigidbody2D>();
		EnemyAnimator=GetComponent<Animator> ();


	}
	void Update(){
		horizontal = Input.GetAxis ("Horizontal");
		if(flyTrigger){
			EnemiesRigidBody.gravityScale=-1;
			showBubbleforFlying.SetActive (true);
			dropCoin ();
			StartCoroutine (coolDown());
		}

		
	
	}

	void MoveEnimies(){
		EnemiesRigidBody.velocity = new Vector2 (horizontal*20,EnemyAnimator.velocity.y);  //x=-1,y=0
		EnemyAnimator.SetFloat("speed",Mathf.Abs(horizontal));
		horizontal = horizontal * 20;
	}

	public static void flyEniemies(Rigidbody2D RigidBody){
		flyTrigger = true;
		//EnemiesRigidBody = RigidBody;


	}
	IEnumerator coolDown()
	{
		yield return new WaitForSeconds (1);
	

	}

	void dropCoin (){
		CoinObject.SetActive (true);
	}



}
