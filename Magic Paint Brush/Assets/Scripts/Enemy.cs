using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/// <summary>
/// Enemy.
/// </summary>
public class Enemy : MonoBehaviour {
	/// <summary>
	/// My rigidbody.
	/// </summary>
	private Rigidbody2D myRigidbody;
	/// <summary>
	/// Gets the animation from the gameobject.
	/// </summary>
	/// <value>My animator.</value>
	public Animator MyAnimator { get; private set;}
	public float movementSpeed;
	private bool facingRight;
	/// <summary>
	/// The currentstate of the enemy.
	/// enemy has different states which we use for different and currentstate should signiify the active enemystate
	/// </summary>
	private IenemyState currentstate;
	/// <summary>
	/// Gets or sets the target.
	/// </summary>
	/// <value>The target.</value>
	 
	public GameObject Target { get; set;}
	/// <summary>
	/// Start this instance.
	/// </summary>

	// Use this for initialization
	void Start () {
		//game start with enemy facing right

		facingRight = true;

		myRigidbody = GetComponent<Rigidbody2D>(); //gets rigidbody and animator
		MyAnimator = GetComponent<Animator> ();

		ChangeState (new IdleState ()); //enters newstate and calls the idlestate interface


	}
	/// <summary>
	/// Update this instance.
	/// </summary>


	
	// Update is called once per frame
	void Update () 
	{
		currentstate.Execute (); // runs the activestate 
		LookAtTarget (); // calls the function to always follow the target which is set as player
	}
	/// <summary>
	/// Looks at target.
	/// reference to the EnemySight class.. Enables enemy collider to detect collision and set the player as target and continuously follow the player.
	/// </summary>
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

	/// <summary>
	/// Changes the direction.
	/// </summary>

	public void ChangeDirection()
	{
		//facingRight = !facingRight; //facingright set to opposite of facing right
			//transform.localScale = new Vector3(transform.localScale.x * -1,1,1);
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	
	
	}
	/// <summary>
	/// Changes the state.
	/// helps the enemy to change the state by calling the newstate interface and run the functions in that active state
	/// </summary>
	/// <param name="newstate">Newstate.</param>
	public void ChangeState (IenemyState newstate ) 

	{
		if (currentstate != null)
		{
			currentstate.Exit (); // exit current state and set a newstate for it
		}
		currentstate = newstate;

		currentstate.Enter (this); //new state becomes current state and it runs what state is entered in the function
		
	}
	/// <summary>
	/// Move this instance.
	/// this enables enemy to move and it can be called anywhere from other interface or Enemystates
	/// </summary>
	public void Move ()
	{
		MyAnimator.SetFloat ("speed", 1);

		transform.Translate (GetDirection() * (movementSpeed * Time.deltaTime));
	}
	/// <summary>
	/// Gets the direction.
	/// helps the enemy to get current direction and return its value to help in the Changedirection () function
	/// </summary>
	/// <returns>The direction.</returns>
	public Vector2 GetDirection()
	{
		return facingRight ? Vector2.right : Vector2.left;
	}
	/// <summary>
	/// Raises the trigger enter2 d event.
	/// </summary>
	/// <param name="other">Other.</param>
	void OnTriggerEnter2D(Collider2D other)
	{
		currentstate.OnTriggerEnter (other);
	}
}
