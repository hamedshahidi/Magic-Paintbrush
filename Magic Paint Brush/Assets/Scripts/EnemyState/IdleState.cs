using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/// <summary>
/// Idle state.
/// </summary>

public class IdleState : IenemyState 
{
	private float IdleTimer;
	private float IdleDuration = 3;
	private Enemy enemy;
	/// <summary>
	/// Execute this instance.
	/// </summary>
	public void Execute ()
	{
		Debug.Log ("i am idling");
		Idle ();

		if (enemy.Target != null) {
			enemy.ChangeState (new PatrolState ()); //enables enemy run or attack player when it spots the target(player)
		} 
	}
	/// <summary>
	/// Enter the specified enemy.
	/// sets this enemy to original enemy when it enters this state
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
	/// </summary>
	/// <param name="other">Other.</param>
	public void OnTriggerEnter (Collider2D other)
	{
		
	}
	/// <summary>
	/// Idle this instance.
	/// sets the amount of time it spends on the idlestate before exiting to a new state called patrolstate
	/// </summary>
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
