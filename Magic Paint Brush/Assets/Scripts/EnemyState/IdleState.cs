using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IdleState : IenemyState 
{
	private float IdleTimer;
	private float IdleDuration = 3;
	private Enemy enemy;

	public void Execute ()
	{
		Debug.Log ("i am idling");
		Idle ();

		if (enemy.Target != null) {
			enemy.ChangeState (new PatrolState ());
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
		
	}
	private void Idle ()
	{
		enemy.MyAnimator.SetFloat ("speed", 0);
		IdleTimer += Time.deltaTime;
		if (IdleTimer >= IdleDuration) 
		{
			enemy.ChangeState (new PatrolState ());
		}
	}
}
