using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //Used for healthBar



public class EnemyScript : MonoBehaviour
{

    public float startingHealth;
    private float currentHealth;

    private int bumpForce = 300;
    private GameObject ball;

    public ParticleSystem collisionParticlePrefab;
    private ParticleSystem tempCollisionParticle;

    public Image healthBar;

    private AudioSource deathSound;

    // Use this for initialization
    void Start ()
    {
        ball = GameObject.FindGameObjectWithTag("Ball");
        currentHealth = startingHealth;

        deathSound = GetComponent<AudioSource>();
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
            GetComponent<Animation>().Play("bounce");
            currentHealth -= 1;
            healthBar.fillAmount = currentHealth/startingHealth;
            if (currentHealth <= 0)
            {   
                tempCollisionParticle = Instantiate(collisionParticlePrefab, transform.position, Quaternion.identity) as ParticleSystem;
                tempCollisionParticle.Play();
                deathSound.Play();
                GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<BoxCollider2D>().enabled = false;
                transform.GetChild(0).gameObject.SetActive(false);
                StartCoroutine(Wait(3));   
            }
        }
    }

    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
