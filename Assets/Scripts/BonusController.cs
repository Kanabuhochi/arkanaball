using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BonusController : MonoBehaviour
{

    public float bonus = 0;
    public Image bonusBar;
    private float maxBonus;
    float bonusDuration = 5f;
    bool bonusObtainable = true;

    // Use this for initialization
    void Start()
    {
        bonusBar = GameObject.Find("Player/BonusBar/Background/Bonus").GetComponent<Image>();
        maxBonus = 30;
        bonusBar.fillAmount = bonus / maxBonus;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (bonusObtainable)
        {
            if (col.gameObject.tag == "Bumper")
            {
                bonus += 10;
                bonusBar.fillAmount = bonus / maxBonus;
                if (bonus >= maxBonus)
                { 

                    bonusObtainable = false;
                    float bonusObtained = Time.time;
                    float time = bonusObtained;      
                    StartCoroutine(ActivateBonus(bonus, 0, bonusDuration));
                    bonusBar.fillAmount = bonus / maxBonus;
                }
            }
        }
    }
    IEnumerator ActivateBonus(float StartPos, float EndPos, float LerpTime)
    {
        Color tmp;
        tmp = GameObject.FindGameObjectWithTag("Ball").GetComponent<SpriteRenderer>().color;
        float StartTime = Time.time;
        float EndTime = StartTime + LerpTime;
        while (Time.time < EndTime)
        {
            char type = GameObject.FindGameObjectWithTag("Ball").GetComponent<BallScript>().type;
            if(type == 'r')
            {
                GameObject.FindGameObjectWithTag("Ball").GetComponent<BallScript>().damage = 2;
            }
            else
            {
                tmp.a = 0.5f;
                GameObject.FindGameObjectWithTag("Ball").GetComponent<BallHealthController>().invincible = true;
                GameObject.FindGameObjectWithTag("Ball").GetComponent<SpriteRenderer>().color = tmp;
            }

            float timeProgressed = (Time.time - StartTime) / LerpTime;
            bonus = Mathf.Lerp(StartPos, EndPos, timeProgressed);
            bonusBar.fillAmount = bonus / maxBonus;
            yield return new WaitForFixedUpdate();
        }
        bonusObtainable = true;
        GameObject.FindGameObjectWithTag("Ball").GetComponent<BallScript>().damage = 1;
        GameObject.FindGameObjectWithTag("Ball").GetComponent<BallHealthController>().invincible = false;
        tmp.a = 1f;
        GameObject.FindGameObjectWithTag("Ball").GetComponent<SpriteRenderer>().color = tmp;

    }
}
