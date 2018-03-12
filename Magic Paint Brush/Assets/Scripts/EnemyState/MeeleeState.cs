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
	public void Enter (Enemy enemy)
	{
		this.enemy = enemy;
	}

	public void Exit ()
	{
		
	}

	public  void OnTriggerEnter (Collider2D other)
	{
		if (other.tag == "edge") 
		{
			enemy.ChangeDirection ();
		}

			
	}



}
