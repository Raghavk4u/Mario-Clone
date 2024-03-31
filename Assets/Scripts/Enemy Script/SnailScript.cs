using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SnailScript : MonoBehaviour
{
    public float moveSpeed = 1f;
    private Rigidbody2D myBody;
    private Animator myAnim;
    private bool moveLeft;
    private bool canMove;
    private bool stunned;
    public LayerMask playerLayer;

    public Transform down_Collision, right_collision, top_collision, left_collision;
    private Vector3 left_collision_Pos, right_collision_Pos;

    private void Awake()
    {
        myAnim = GetComponent<Animator>();
        myBody = GetComponent<Rigidbody2D>();

        left_collision_Pos = left_collision.position;
        right_collision_Pos = right_collision.position;
    }
    void Start()
    {
        moveLeft = true;
        canMove = true;
    }
    void Update()
    {

        if(canMove)
        { 
            if (moveLeft)
            {
                myBody.velocity = new Vector2(-moveSpeed, myBody.velocity.y);
            }
            else
            {
                myBody.velocity = new Vector2(moveSpeed, myBody.velocity.y);

            }
        }
        checkCollision();
    }
    void checkCollision()
    {
        RaycastHit2D leftHit = Physics2D.Raycast(left_collision.position, Vector2.left, 0.1f, playerLayer);
        RaycastHit2D rightHit = Physics2D.Raycast(right_collision.position, Vector2.right, 0.1f, playerLayer);

        Collider2D topHit = Physics2D.OverlapCircle(top_collision.position, 0.2f, playerLayer);  // creates the circular raycast(collider) second arguement is for radius

        if (topHit != null)
        {
            if (topHit.gameObject.tag == Mytags.PLAYER_TAG)
            {
                if (!stunned)
                {
                    topHit.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(topHit.gameObject.GetComponent<Rigidbody2D>().velocity.x, 7f);
                    myBody.velocity = Vector2.zero;
                    myAnim.Play("Stunned");
                    canMove = false;
                    stunned = true;

                    //SAME CODE CAN BE REUSED FOR BEETLE

                    if (tag == Mytags.BEETLE_TAG)
                    {                                  // tag = gameObject.tag
                        myAnim.Play("Stunned");
                        StartCoroutine(Dead(0.5f));
                    }
                }
            }
        }

        if (leftHit)
        {
            if (leftHit.collider.tag == Mytags.PLAYER_TAG)
            {
                if (!stunned)
                {
                    //Apply damage

                    leftHit.collider.gameObject.GetComponent<PlayerDamage>().DealDamage();
                }
                else
                {
                    if (tag != Mytags.BEETLE_TAG)
                    {
                        myBody.velocity = new Vector2(15f, myBody.velocity.y);
                        StartCoroutine(Dead(1f));
                    }
                }
            }
        }

        if (rightHit)
        {
            if (rightHit.collider.tag == Mytags.PLAYER_TAG)
            {
                if (!stunned)
                {
                    //Apply damage

                    rightHit.collider.gameObject.GetComponent<PlayerDamage>().DealDamage();
                }
                else
                {
                    if (tag != Mytags.BEETLE_TAG)
                    {
                        myBody.velocity = new Vector2(-15f, myBody.velocity.y);
                        StartCoroutine(Dead(1f));
                    }
                }
            }

        }

        if (!Physics2D.Raycast(down_Collision.position, Vector2.down, 1f))
        {
            ChangeDirection();
        }
    }
    void ChangeDirection()
    {
        moveLeft = !moveLeft;
        Vector3 tempScale = transform.localScale;

        if (moveLeft)
        {
            tempScale.x = Mathf.Abs(tempScale.x);

            left_collision_Pos = left_collision.position;
            right_collision_Pos = right_collision.position;
        }
        else
        {
            tempScale.x = -Mathf.Abs(tempScale.x);
            left_collision_Pos = right_collision.position;
            right_collision_Pos = left_collision.position;

        }
        transform.localScale = tempScale;
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
            if(tag == Mytags.BEETLE_TAG)
            {
                myAnim.Play("Stunned");
                canMove = false;

                myBody.velocity = Vector2.zero;
                StartCoroutine(Dead(0.4f));
            }

            if(tag == Mytags.SNAIL_TAG)
            {
                if (!stunned)
                {
                    myAnim.Play("Stunned");
                    canMove = false;
                    myBody.velocity = Vector2.zero;
                    stunned = true;
                }
                else
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
