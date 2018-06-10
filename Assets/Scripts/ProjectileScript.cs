using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour {

    public Rigidbody2D rb;
    private float created;
    private float duration;

	// Use this for initialization
	void Start ()
    {
        created = Time.time;
        duration = 0.25f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        GetComponent<Animation>().Play("rotateFlame");
        transform.localScale += (new Vector3(1, 1, 1) * Time.deltaTime);

        if (Time.time - created > duration)
            Destroy(gameObject);
	}
}
