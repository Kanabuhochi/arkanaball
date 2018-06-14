using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public GameObject enemy2;
    public GameObject levelImage;
    public int enemies = 0;
    public int wave = 0;

    private GameObject typo;

    private int level = 0;

    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        enemies = 3;
        Spawn();
        levelImage = GameObject.Find("LevelImage");
        levelImage.SetActive(false);
        GameObject.Find("ScoreManager").GetComponent<ScoreManager>().Show();
        GameObject.Find("ScoreManager").GetComponent<ScoreManager>().Begin();

    }

    // Update is called once per frame
    void Update()
    {
        if (enemies == 0)
        {
            wave += 1;
            if (wave == 3)
            {
                enemies = 3;
                level++;
                wave = 0;
                ChangeLevel();
                GameObject.Find("ScoreManager").GetComponent<ScoreManager>().Show();
                Invoke("Spawn", 1.5f);
            }
            else
            {
                enemies = 3;
                StartCoroutine(Wait(3));
                Invoke("Spawn", 1.5f);
            }

        }
    }

    void ChangeLevel()
    {
        GameObject.Find("ScoreManager").GetComponent<ScoreManager>().Hide();
        levelImage.SetActive(true);
        int lvl = Random.Range(1, 8);
        SceneManager.LoadScene("S" + lvl);
        Invoke("HideLevelImage", 2f);


    }

    private void HideLevelImage()
    {
        levelImage.SetActive(false);
    }

    void Spawn()
    {

        Vector2 spawn = new Vector2(0.06f, 3.63f);
        Instantiate(Type(), spawn, Quaternion.identity);
        spawn = new Vector2(-1.69f, 1.19f);
        Instantiate(Type(), spawn, Quaternion.identity);
        spawn = new Vector2(1.95f, 1.19f);
        Instantiate(Type(), spawn, Quaternion.identity);
    }

    GameObject Type()
    {
        int type = Random.Range(0, 2);
        //Debug.Log(type);
        if (type == 0)
        {
            typo = enemy;
        }

        else if (type == 1)
        {
            typo = enemy2;

        }
        return typo;
    }

    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
    }
}