using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MeeleeState : IenemyState 
// calls the interface Ienemystate
//the class that implements this interfaace Ienemystate will need to implement the functions in the Ienemystate
{
	private Enemy enemy;


	/// <summary>
	/// Execute this instance.
	/// if enemy spots the target in this state it tries to attack the player if not it immediately switches back to the idlestate
	/// </summary>
	public void Execute ()
	{
		Debug.Log ("attacking");

		if (enemy.Target != null)
		{
			enemy.Move ();

		} else 
		{
			enemy.ChangeState (new IdleState ());
		}
	}
	/// <summary>
	/// Enter the specified enemy.
	/// enters this state as the enemy
	/// </summary>
	/// <param name="enemy">Enemy.</param>
	public void Enter (Enemy enemy)
	{
		this.enemy = enemy;
	}

	public void Exit ()
	{
		
	}
	/// <summary>
	/// Raises the trigger enter event.
	/// enables it detect edges and change direction while on this state
	/// </summary>
	/// <param name="other">Other.</param>

	public  void OnTriggerEnter (Collider2D other)
	{
		if (other.tag == "edge") 
		{
			enemy.ChangeDirection ();
		}

			
	}



}
