using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject[] balls;
    private GameObject ball;

    private Vector3 fp;   //First touch position
    private Vector3 lp;   //Last touch position
    private float dragDistance;  //minimum distance for a swipe to be registered
    public int i = 0;
    public int ballsCounter = 2;

    private float[] healths = new float[2];
    private float[] bonuses = new float[2];


    // Use this for initialization
    void Start()
    {
        healths[0] = 2f;
        healths[1] = 2f;
        bonuses[0] = 0f;
        bonuses[1] = 0f;
        DontDestroyOnLoad(this.gameObject);
        ball = Instantiate(balls[0], new Vector3(0, -4.31f, 0), Quaternion.identity);
        ball.transform.parent = gameObject.transform;
        ball.GetComponent<BallHealthController>().currentHealth = healths[0];
        ball.GetComponent<BonusController>().bonus = bonuses[0];
        dragDistance = Screen.height * 15 / 100; //dragDistance is 15% height of the screen
    }




    // Update is called once per frame
    void Update()
    {
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
                    if (lp.y > fp.y)  //If the movement was up
                    {   //Up swipe
                        Debug.Log("Up Swipe");

                    }
                    else
                    {   //Down swipe
                        Debug.Log("Down Swipe");
                        changeBall();
                        ball.transform.parent = gameObject.transform;
                    }
                }
                else
                {   //It's a tap as the drag distance is less than 20% of the screen height
                    Debug.Log("Tap");
                }
            }
        }

    }

    public void changeBall()
    {
        if (i == 1 && healths[0] > 0)
        {
            healths[1] = GameObject.FindGameObjectWithTag("Ball").GetComponent<BallHealthController>().currentHealth;
            bonuses[1] = GameObject.FindGameObjectWithTag("Ball").GetComponent<BonusController>().bonus;
            GameObject.FindGameObjectWithTag("Ball").GetComponent<BallScript>().Delete();
            ball = Instantiate(balls[0], new Vector3(0, -4.31f, 0), Quaternion.identity);
            ball.GetComponent<BallHealthController>().currentHealth = healths[0];
            ball.GetComponent<BonusController>().bonus = bonuses[0];
            i = 0;
        }
        else if (i == 0 && healths[1] > 0)
        {
            healths[0] = GameObject.FindGameObjectWithTag("Ball").GetComponent<BallHealthController>().currentHealth;
            bonuses[0] = GameObject.FindGameObjectWithTag("Ball").GetComponent<BonusController>().bonus;
            GameObject.FindGameObjectWithTag("Ball").GetComponent<BallScript>().Delete();
            ball = Instantiate(balls[1], new Vector3(0, -4.31f, 0), Quaternion.identity);
            ball.GetComponent<BallHealthController>().currentHealth = healths[1];
            ball.GetComponent<BonusController>().bonus = bonuses[1];
            i = 1;
        }
    }
}

