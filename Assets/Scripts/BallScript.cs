using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{

    public Rigidbody2D rb;
    public CircleCollider2D cc;
    public float ballForce;
    private bool turn;

    private float maxVelocity = 15;
    private float sqrMaxVelocity;

    // Use this for initialization
    void Start ()
    {
        SetMaxVelocity(maxVelocity);
        turn = false;
    }

    void SetMaxVelocity(float maxVelocity)
    {
        this.maxVelocity = maxVelocity;
        sqrMaxVelocity = maxVelocity * maxVelocity;
    }

    // Update is called once per frame
    void Update ()
    {

        var v = rb.velocity;
        // Clamp the velocity, if necessary
        // Use sqrMagnitude instead of magnitude for performance reasons.
        if (v.sqrMagnitude > sqrMaxVelocity)
            rb.velocity = v.normalized * maxVelocity;

            if (rb.position.y < -3.3 && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            cc.sharedMaterial = (PhysicsMaterial2D)Resources.Load("Bounce");
            rb.velocity = new Vector2(-rb.position.x, 10);
        }
        if(cc.sharedMaterial == (PhysicsMaterial2D)Resources.Load("Slide"))
        {
            
            if (turn == false)
                rb.velocity = new Vector2(6, (float)-3);
            else
                rb.velocity = new Vector2(-6, (float)-3);
            if (rb.position.x > 2.7)
                turn = true;
            else if (rb.position.x < -2.7)
                turn = false;
        }

        if(rb.position.y <= (float)-4.41)
        {
            cc.sharedMaterial = (PhysicsMaterial2D)Resources.Load("Slide");
            if (rb.velocity.x > 0)
                turn = false;
            else if (rb.velocity.x < 0)
                turn = true;
            rb.velocity = new Vector2(0, 0);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
     {
         if (col.gameObject.name == "Slider")
         {
            if (rb.velocity.x > 0)
                turn = false;
            else if (rb.velocity.x < 0)
                turn = true;
            rb.velocity = new Vector2(0, 0);
            cc.sharedMaterial = (PhysicsMaterial2D)Resources.Load("Slide");
         }

    }

    
}
