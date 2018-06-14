using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //Used for healthBar



public class EnemyScript : MonoBehaviour
{

    public float startingHealth;
    private float currentHealth;
    public GameObject ball;

    private int bumpForce = 300;

    public ParticleSystem collisionParticlePrefab;
    private ParticleSystem tempCollisionParticle;

    public Image healthBar;
    public char type;

    private AudioSource deathSound;

    // Use this for initialization
    void Start ()
    {
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
        if (col.gameObject.tag == "Ball")
        {
            float damageMultiplier = 1f;
            if(GameObject.FindGameObjectWithTag("Ball").GetComponent<BallScript>().type == type)
            {
            damageMultiplier = 0.5f;
            }
            ball = GameObject.FindGameObjectWithTag("Ball");
            AddExplosionForce(ball, bumpForce);
            GetComponent<Animation>().Play("bounce");
            currentHealth -= GameObject.FindGameObjectWithTag("Ball").GetComponent<BallScript>().damage * damageMultiplier;
            healthBar.fillAmount = currentHealth/startingHealth;
            if (currentHealth <= 0)
            {   
                tempCollisionParticle = Instantiate(collisionParticlePrefab, transform.position, Quaternion.identity) as ParticleSystem;
                tempCollisionParticle.Play();
                AudioClip clip = Resources.Load("Audio/grunt1") as AudioClip;
                deathSound.PlayOneShot(clip);
                GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<BoxCollider2D>().enabled = false;
                transform.GetChild(0).gameObject.SetActive(false);
                StartCoroutine(Wait(3));
                GameObject.Find("Spawner").GetComponent<EnemySpawner>().enemies -= 1;
                GameObject.Find("ScoreManager").GetComponent<ScoreManager>().IncrementScore();
            }
        }
    }

    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
