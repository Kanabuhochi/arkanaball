using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemyScript : MonoBehaviour
{

    private int health;
    private int bumpForce = 300;
    private GameObject ball;

    public ParticleSystem collisionParticlePrefab; //Assign the Particle from the Editor (You can do this from code too)
    private ParticleSystem tempCollisionParticle;

    // Use this for initialization
    void Start ()
    {
        ball = GameObject.FindGameObjectWithTag("Ball");
        health = 3;
	}
	
	// Update is called once per frame
	void Update ()
    {
        
    }

    public void AddExplosionForce(GameObject body, float explosionForce)
    {
        var dir = (body.transform.position - this.transform.position);
        body.GetComponent<Rigidbody2D> ().AddForce(dir.normalized * explosionForce);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject == ball)
        {
            AddExplosionForce(ball, bumpForce);
            GetComponent<Animation> ().Play("bounce");
            tempCollisionParticle = Instantiate(collisionParticlePrefab, transform.position, Quaternion.identity) as ParticleSystem;
            tempCollisionParticle.Play();
            health -= 1;
            if (health <= 0)
                Destroy(gameObject);

        }
    }
}
