using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemyScript : MonoBehaviour {

    private int health;

    // Use this for initialization
    void Start () {
        health = 3;
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "PlayerBall")
        {
            health -= 1;
            if (health <= 0)
                Destroy(gameObject);
        }
    }
}
