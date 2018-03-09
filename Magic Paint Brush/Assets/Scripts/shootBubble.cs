using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class shootBubble : MonoBehaviour {
	[SerializeField]
	private float speed;
	private Rigidbody2D myRidigbody;
	private Vector2 direction;


	// Use this for initialization
	void Start () {
		myRidigbody = GetComponent<Rigidbody2D>();

		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		myRidigbody.velocity = direction * speed;

	}
	public void Initialize(Vector2 direction){
		this.direction = direction;
	
	}
	void OnBecaeInvisible(){
		Destroy (gameObject);
	}
}
