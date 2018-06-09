using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class ScoreManager : MonoBehaviour {
    int score = 0;
    public Text scoreText;
	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this.gameObject);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void IncrementScore()
    {
        score++;
        scoreText.text = "Score: " + score;
    }

    public void Hide()
    {
        scoreText.fontSize = 0;
    }

    public void Begin()
    {
        scoreText.transform.position = new Vector2(206,20);
    }

    public void Show()
    {
        scoreText.fontSize = 32;
    }
}
