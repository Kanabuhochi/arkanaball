using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyAttackCotroller : MonoBehaviour {

    public int numberOfProjectiles;
    private float attackDelay = 5;
    private float shootingDelay = 0.2f;
    private float lastAttack = 0;
    public GameObject flame;
    private SpriteRenderer mainRenderer;
    private AudioSource sound;

    // Use this for initialization
    void Start ()
    {
        sound = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Time.time > lastAttack + attackDelay)
        {
            int i;
            for (i = 0; i < 10; i++)
            {
                Invoke("SignalAttack", i*0.1f);
            }
            Invoke("Shoot", 1f);
            for (i = 1; i < numberOfProjectiles; i++)
            {
                Invoke("Shoot", 1f + (i * shootingDelay));
            }
            if (i == numberOfProjectiles)
                lastAttack = Time.time;
        }         
    }

    void Shoot()
    {
        GameObject newObject = Instantiate(flame, transform.position,transform.rotation) as GameObject;
        newObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

    }

    void SignalAttack()
    {
        AudioClip clip = (AudioClip)Resources.Load("Audio/grunt4");
        sound.PlayOneShot(clip);
        GetComponent<Animation>().Play("signalAttack");
    }
}
