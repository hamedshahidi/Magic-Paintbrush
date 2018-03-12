using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class shootBubble : MonoBehaviour {
	[SerializeField]
	private float speed;
	private Rigidbody2D myRidigbody;
	private Vector2 direction;
	private bool shoot;
	public GameObject bubble;


	// Use this for initialization
	void Start () {
		shoot = true;
		myRidigbody = GetComponent<Rigidbody2D>();


	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (shoot) {
			StartCoroutine (coolDown ());
		} else {
			Destroy (bubble);

		}


	}
	public void Initialize(Vector2 direction){
		this.direction = direction;
	
	}

	void OnTriggerEnter2D(Collider2D other){

		if (other.tag == "die") {
			

			AudioScript.PlaySound ("coin");
			EnemiesScript.flyEniemies (other.gameObject.GetComponent<Rigidbody2D>());
			StartCoroutine (waitforEnemyDestroy(other.gameObject));

		}




	}
	void OnBecaeInvisible(){
		Destroy (gameObject);
	}

	IEnumerator coolDown()
	{
		myRidigbody.velocity = direction * speed;
		yield return new WaitForSeconds (1);
		shoot = false;

	}

	IEnumerator waitforEnemyDestroy(GameObject other)
	{
		
		yield return new WaitForSeconds (1);
		Destroy(other.gameObject);

	}
}
