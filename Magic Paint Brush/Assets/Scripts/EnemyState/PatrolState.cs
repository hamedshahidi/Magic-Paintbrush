using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/// <summary>
/// Patrol state.
/// </summary>
public class PatrolState : IenemyState // here the patrolstate calls the interface Ienemystate
{
	private float patrolTimer;
	private float patrolDuration = 5;
	private Enemy enemy;
	/// <summary>
	/// Execute this instance.
	/// </summary>
	public void Execute ()
	{
		Debug.Log ("patrolling");
		Patrol ();
		enemy.Move ();
		if (enemy.Target != null) 
		{
			enemy.ChangeState (new MeeleeState ()); 
		} 
	}
	/// <summary>
	/// Enter the specified enemy.
	/// enables this patrolstate interface become the enemy when this state is active
	/// </summary>
	/// <param name="enemy">Enemy.</param>
	public void Enter (Enemy enemy)
	{
		this.enemy = enemy;
	}
	/// <summary>
	/// Exit this instance.
	/// </summary>

	public void Exit ()
	{
		
	}
	/// <summary>
	/// Raises the trigger enter event.
	/// enables the enemy recognize an edge collision and change direction
	/// </summary>
	/// <param name="other">Other.</param>
	public void OnTriggerEnter (Collider2D other)
	{
		if (other.tag == "edge") 
		{
			enemy.ChangeDirection ();
		}
		
	}
		/// <summary>
		/// Patrol this instance.
	    /// enables enemy stay in the patrol state for a fixed duration of time before exiting to the idlestate
		/// </summary>
	private void Patrol ()
	{
		enemy.MyAnimator.SetFloat ("speed", 0);
		patrolTimer += Time.deltaTime;
		if (patrolTimer >= patrolDuration) 
		{
			enemy.ChangeState (new IdleState ());
		}
	}


}
