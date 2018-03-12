using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Ienemy state.
/// enemy interface which all enemy states must use and functions below
/// </summary>
//interface which other enemystates are going to use the functions below to execute their state
public interface IenemyState 
{
	void Execute ();
	void Enter (Enemy enemy);// helps the enemy to know which state it is
	void Exit ();
	void OnTriggerEnter(Collider2D other);

	
}
