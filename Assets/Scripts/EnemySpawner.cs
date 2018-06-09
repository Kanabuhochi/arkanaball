using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    public GameObject enemy;
    public GameObject levelImage;
    public int enemies = 0;

    private int level = 0;

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this.gameObject);
        enemies = 3;
        Spawn();
        levelImage = GameObject.Find("LevelImage");
        levelImage.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {

        if(enemies == 0)
        {
            enemies = 3;
            level++;
            ChangeLevel();
            Invoke("Spawn",1.5f);
        }
		
	}

    void ChangeLevel()
    {
        levelImage.SetActive(true);
        Invoke("HideLevelImage", 2f);
        SceneManager.LoadScene("S"+level);
        
    }

    private void HideLevelImage()
    {
        levelImage.SetActive(false);
    }

    void Spawn()
    {
        Debug.Log("Spawn Called");
        Vector2 spawn = new Vector2(0.06f,3.63f);
        Instantiate(enemy, spawn, Quaternion.identity);
        spawn = new Vector2(-1.69f, 1.19f);
        Instantiate(enemy, spawn, Quaternion.identity);
        spawn = new Vector2(1.95f, 1.19f);
        Instantiate(enemy, spawn, Quaternion.identity);
    }

}
