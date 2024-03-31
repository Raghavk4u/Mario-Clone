using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerDamage : MonoBehaviour
{
    private int lifeScoreCount;
    private Text lifeText;

    private bool canDamage;

    private void Awake()
    {
        lifeText = GameObject.Find("LifeText").GetComponent<Text>();
        lifeScoreCount = 3;
        lifeText.text = "x" + lifeScoreCount;

        canDamage = true;
    }
    private void Start()
    {
        Time.timeScale = 1.0f;
    }
    public void DealDamage()
    {
        if(canDamage)
        {
            lifeScoreCount--;

            if(lifeScoreCount >= 0)
            {
                lifeText.text = "x" + lifeScoreCount;
            }
            if(lifeScoreCount == 0)
            {
                //Restart the game
                Time.timeScale = 0f;
                StartCoroutine(RestartGame());
            }

            canDamage = false;

            StartCoroutine(WaitForDamage());
        }
    }

    IEnumerator WaitForDamage()
    {
        yield return new WaitForSeconds(2f);
        canDamage = true;
    }

    IEnumerator RestartGame()
    {
        yield return new WaitForSecondsRealtime(2f);
        SceneManager.LoadScene("GamePlay");
    }

    
}
