using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    private Vector3 fp;   //First touch position
    private Vector3 lp;   //Last touch position
    private float dragDistance;  //minimum distance for a swipe to be registered

    private int swipes;

    public Rigidbody2D rb;
    public CircleCollider2D cc;
    public float ballForce;
    private bool turn;

    private float maxVelocity = 15;
    private float sqrMaxVelocity;

    // Use this for initialization


    void Start ()
    {
        DontDestroyOnLoad(this.gameObject);
       
        dragDistance = Screen.height *25 / 100; //dragDistance is 15% height of the screen
        SetMaxVelocity(maxVelocity);
        turn = false;
        swipes = 1;
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
        else
        {


            float xx = Input.acceleration.x;

            if (xx < -0.5 && swipes > 0 && cc.sharedMaterial == (PhysicsMaterial2D)Resources.Load("Bounce"))
            {
                rb.velocity = new Vector2(rb.velocity.x - 3, rb.velocity.y);
                swipes--;
                Debug.Log(xx);
            }
            else if (xx > 0.5 && swipes > 0 && cc.sharedMaterial == (PhysicsMaterial2D)Resources.Load("Bounce"))
            {
                Debug.Log(xx);
                rb.velocity = new Vector2(rb.velocity.x + 3, rb.velocity.y);
                swipes--;
            }

            if (Input.touchCount == 1) // user is touching the screen with a single touch
            {
                Touch touch = Input.GetTouch(0); // get the touch
                if (touch.phase == TouchPhase.Began) //check for the first touch
                {
                    fp = touch.position;
                    lp = touch.position;
                }
                else if (touch.phase == TouchPhase.Moved) // update the last position based on where they moved
                {
                    lp = touch.position;
                }
                else if (touch.phase == TouchPhase.Ended) //check if the finger is removed from the screen
                {
                    lp = touch.position;  //last touch position. Ommitted if you use list

                    //Check if drag distance is greater than 20% of the screen height
                    if (Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance)
                    {//It's a drag
                     //check if the drag is vertical or horizontal
                        if (Mathf.Abs(lp.x - fp.x) > Mathf.Abs(lp.y - fp.y))
                        {   //If the horizontal movement is greater than the vertical movement...
                            if ((lp.x > fp.x))  //If the movement was to the right)
                            {   //Right swipe

                                if (swipes > 0)
                                {
                                    Debug.Log("swipe right");
                                    rb.velocity = new Vector2(rb.velocity.x + 3, rb.velocity.y);
                                    swipes--;
                                }
                            }
                            else
                            {   //Left swipe

                                if (swipes > 0)
                                {
                                    Debug.Log("swipe left");
                                    rb.velocity = new Vector2(rb.velocity.x - 3, rb.velocity.y);
                                    swipes--;
                                }
                            }
                        }
                    }
                }
            }
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
            swipes = 1;
            if (rb.velocity.x > 0)
                turn = false;
            else if (rb.velocity.x < 0)
                turn = true;
            rb.velocity = new Vector2(0, 0);
            cc.sharedMaterial = (PhysicsMaterial2D)Resources.Load("Slide");
         }

    }

    
}
