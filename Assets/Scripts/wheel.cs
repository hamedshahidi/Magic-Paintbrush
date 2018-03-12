using UnityEngine;
using System.Collections;


public class wheel : MonoBehaviour {

    public float maxspeed = 500f;
    public float minspeed = 100f;
<<<<<<< HEAD
    public static float speed;
=======
    float speed;
>>>>>>> 41dd23dd2d157aaad55530e5984f86ee544da640
    private float position = 0.0f;
	private float initradius = 4.75f;
	private float minradius = 4f;
	private CircleCollider2D collider;

 
        // Use this for initialization
        void Start () {
		collider = transform.GetComponent<CircleCollider2D>();
        speed = Random.Range(minspeed, maxspeed);
 
}

    // Update is called once per frame
    void FixedUpdate() {
        if (speed != 0)
        {
            speed *= 0.99f;
            if (speed < 0.01f)
            { speed = 0f; }
            if (collider.radius > minradius)
                collider.radius *= 0.99f;
            else
                collider.radius = minradius;
            position -= speed;
            position = position % 360;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, position), 1);
        }
	}
}
