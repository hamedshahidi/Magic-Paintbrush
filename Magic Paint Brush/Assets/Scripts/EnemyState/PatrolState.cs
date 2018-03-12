using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PatrolState : IenemyState // here the patrolstate calls the interface Ienemystate
{
	private float patrolTimer;
	private float patrolDuration = 5;
	private Enemy enemy;

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
	public void Enter (Enemy enemy)
	{
		this.enemy = enemy;
	}


	public void Exit ()
	{
		
	}

	public void OnTriggerEnter (Collider2D other)
	{
		if (other.tag == "edge") 
		{
			enemy.ChangeDirection ();
		}
		
	}
		
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
