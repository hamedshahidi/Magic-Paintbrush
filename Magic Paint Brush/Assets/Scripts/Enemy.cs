using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour {
	private Rigidbody2D myRigidbody;

	public Animator MyAnimator { get; private set;}
	public float movementSpeed;
	private bool facingRight;

	private IenemyState currentstate;  // enemy has different states which we use for different and currentstate should signiify the active enemystate 
	public GameObject Target { get; set;}//

	// Use this for initialization
	void Start () {
		//game start with enemy facing right
		facingRight = true;

		myRigidbody = GetComponent<Rigidbody2D>();
		MyAnimator = GetComponent<Animator> ();


		ChangeState (new IdleState ());


	}



	
	// Update is called once per frame
	void Update () 
	{
		currentstate.Execute ();
		LookAtTarget ();
	}
	private void LookAtTarget()
	{
		if (Target != null) // if it sees an object
		{
			float xDir = Target.transform.position.x - transform.position.x; // target position minus enemy position
			if (xDir < 0 && facingRight || xDir > 0 && !facingRight) // if the result is less than 0 then target is on the left but if it is > 0 then target is on the right
			{
				ChangeDirection ();
			} 


			
		}
	}



	public void ChangeDirection()
	{
		facingRight = !facingRight; //facingright set to opposite of facing right
			transform.localScale = new Vector3(transform.localScale.x * -1,1,1);

	
	}
	public void ChangeState (IenemyState newstate ) 
	// helps the enemy to change the state and run the functions in that active state
	{
		if (currentstate != null)
		{
			currentstate.Exit (); // exit current state and set a newstate for it
		}
		currentstate = newstate;

		currentstate.Enter (this); //new state becomes current state and it runs what state is entered in the function
		
	}
	public void Move ()
	{
		MyAnimator.SetFloat ("speed", 1);

		transform.Translate (GetDirection() * (movementSpeed * Time.deltaTime));
	}
	public Vector2 GetDirection()
	{
		return facingRight ? Vector2.right : Vector2.left;
	}
	void OnTriggerEnter2D(Collider2D other)
	{
		currentstate.OnTriggerEnter (other);
	}
}
