using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
   private Text coinTextScore;
    private AudioSource audio1;
    private int score;
    private void Awake()
    {
        audio1 = GetComponent<AudioSource>();
    }
    void Start()
    {

        coinTextScore = GameObject.Find("CoinText").GetComponent<Text>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == Mytags.COIN_TAG)
        {
            collision.gameObject.SetActive(false);
            score++;
             //Debug.Log("x" +  score);
            coinTextScore.text = "x" + score;

            audio1.Play();

        }
    }
}
