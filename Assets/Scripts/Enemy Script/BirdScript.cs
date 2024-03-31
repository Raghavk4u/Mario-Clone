using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScript : MonoBehaviour
{
    private Rigidbody2D myBody;
    private Animator myAnim;

    private Vector3 moveDirection = Vector3.left;
    private Vector3 originPosition;
    private Vector3 movePosition;

    public GameObject birdEgg;
    public LayerMask playerLayer;
    private bool attacked;
    private bool canMove;

    private float speed = 2.5f;

    private void Awake()
    {
        myAnim = GetComponent<Animator>();
        myBody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        originPosition = transform.position;
        originPosition.x += 6f;

        movePosition = transform.position;
        movePosition.x += -6f;
        canMove = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveTheBird();
        DropEgg();
    }

    void MoveTheBird()
    {
        if (canMove)
        {
            transform.Translate(moveDirection * speed* Time.smoothDeltaTime);

            if(transform.position.x >= originPosition.x)
            {
                moveDirection = Vector3.left;
                ChangeDirection(0.5f);
            }else if(transform.position.x <= movePosition.x)
            {
                moveDirection = Vector3.right;
                ChangeDirection(-0.5f);
            }
        }
    }

    void ChangeDirection(float direction)
    {
        Vector3 temp = transform.localScale;
        temp.x = direction;
        transform.localScale = temp;
    }

    void DropEgg()
    {
        if(!attacked)
        {
            if (Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, playerLayer))
            { 
                 Instantiate(birdEgg,new Vector3(transform.position.x,transform.position.y - 1f, transform.position.z),Quaternion.identity);
                attacked = true;
                myAnim.Play("BirdFly");
            }
        }
    }

    IEnumerator Dead(float timer)
    {
        yield return new WaitForSeconds(timer); 
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == Mytags.BULLET_TAG)
        {
            GetComponent<BoxCollider2D>().isTrigger = true;
            myBody.bodyType = RigidbodyType2D.Dynamic;
            canMove = false;
            myAnim.Play("BirdDead");
            StartCoroutine(Dead(3f));
        }
    }
}
