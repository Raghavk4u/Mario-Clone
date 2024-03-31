using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float speed = 5f;
    private Rigidbody2D myBody;
    private Animator myAnim;
    public Transform groundCheckPositon;
    public LayerMask groundCheckLayer;
    private bool isGrounded , jumped;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckGrounded();
        PlayerJump();
    }

    private void FixedUpdate()
    {
        PlayerWalk();
    }

    void PlayerWalk()
    {
        float h = Input.GetAxisRaw("Horizontal");
        if(h > 0)
        {
            myBody.velocity = new Vector2(speed , myBody.velocity.y);
            ChangeDirection(1); 

        }else if(h < 0)
        {
             myBody.velocity = new Vector2(-speed , myBody.velocity.y);
            ChangeDirection(-1);
        }
        else
        {
            myBody.velocity = new Vector2(0f, myBody.velocity.y);
        }

        myAnim.SetInteger("Speed", Mathf.Abs((int)myBody.velocity.x));
    }


    void ChangeDirection(int direction)
    {
        Vector3 tempScale = transform.localScale;
        tempScale.x = direction;
        transform.localScale = tempScale;
    }

    void CheckGrounded()
    {
        isGrounded = Physics2D.Raycast(groundCheckPositon.position, Vector2.down,0.5f,groundCheckLayer);
        if(isGrounded)
        {
            if (jumped)
            {
                jumped = false;
                myAnim.SetBool("Jump", false);

            }
        }
    }

    void PlayerJump()
    {
        if (isGrounded)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                jumped = true;
                myBody.velocity = new Vector2(myBody.velocity.x, 10f);
                myAnim.SetBool("Jump", true);
            }

        }
    }

}
