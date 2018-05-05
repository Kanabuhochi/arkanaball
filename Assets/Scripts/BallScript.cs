using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{

    public Rigidbody2D rb;
    public CircleCollider2D cc;
    public float ballForce;

    // Use this for initialization
    void Start ()
    {
        rb.AddForce(new Vector2(ballForce, ballForce));
	}
	
	// Update is called once per frame
	void Update ()
    {

		
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "Slider")
        {
                this.cc.sharedMaterial = null;
                this.rb.gravityScale = 1;
        }
    }
}
