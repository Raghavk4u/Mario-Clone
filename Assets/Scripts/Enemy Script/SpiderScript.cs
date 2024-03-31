using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SpiderScript : MonoBehaviour
{
    private Animator myAnim;
    private Rigidbody2D mybody;

    private Vector3 moveDirection = Vector3.down;
    private string coroutine_name = "ChangeMovement";

    private void Awake()
    {
        myAnim = GetComponent<Animator>();
        mybody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        StartCoroutine(coroutine_name);
    }

    // Update is called once per frame
    void Update()
    {
        MoveSpider();
    }

    void MoveSpider()
    {
        transform.Translate(moveDirection * Time.deltaTime);
    }

    IEnumerator ChangeMovement()
    {
        yield return new WaitForSeconds(Random.Range(2f,5f));

        if(moveDirection == Vector3.down)
        {
            moveDirection = Vector3.up;
        }
        else
        {
            moveDirection = Vector3.down;
        }

        StartCoroutine(coroutine_name);
    }

    IEnumerator SpiderDead()
    {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == Mytags.BULLET_TAG) {
            myAnim.Play("SpiderDead");
            mybody.bodyType = RigidbodyType2D.Dynamic;
            StartCoroutine(SpiderDead());
            StopCoroutine(coroutine_name);
            
        }

        if(collision.gameObject.tag == Mytags.PLAYER_TAG)
        {
            collision.GetComponent<PlayerDamage>().DealDamage();
        }
    }
}
