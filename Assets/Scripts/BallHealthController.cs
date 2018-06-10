using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BallHealthController : MonoBehaviour {

    public float startingHealth;
    private float currentHealth;
    public Image healthBar;
    bool invincible = false;

    // Use this for initialization
    void Start ()
    {
        currentHealth = startingHealth;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator OnTriggerEnter2D(Collider2D col)
    {
        if (!invincible)
        {
            if (col.gameObject.tag == "Projectile")
            {
                currentHealth--;
                healthBar.fillAmount = currentHealth / startingHealth;
                if (currentHealth <= 0)
                {
                    
                    SceneManager.LoadScene("zGameOver");
                    Invoke("Restart", 3f);
                }
                GetComponent<Animation>().Play("ballBlinking");
                invincible = true;
                yield return new WaitForSeconds(3);
                invincible = false;
            }
        }
    }

    private void Restart()
    {
        Debug.Log("restart");
        SceneManager.LoadScene("Menu");
        Destroy(gameObject);
        Destroy(GameObject.Find("ScoreManager"));
    }
}
