using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    private float speed = 10f;
    private Animator anim;
    private bool canMove;

    private void Awake()
    {
        anim = GetComponent<Animator>(); 
    }
    void Start()
    {
        canMove = true;
        StartCoroutine(DisableBullet(5f));        // if the bullet doesn't hit the target 
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
       if(canMove)
        {
            Vector3 temp = transform.position;
            temp.x += speed * Time.deltaTime;
            transform.position = temp;
        }
    }
    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    IEnumerator DisableBullet(float timer)
    {
        yield return new WaitForSeconds(timer);
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == Mytags.SNAIL_TAG || other.gameObject.tag == Mytags.BEETLE_TAG ||other.gameObject.tag == Mytags.SPIDER_TAG)
        { 
            canMove=false;
            anim.Play("Explode");
            StartCoroutine(DisableBullet(0.9f));

        }
    }
}
