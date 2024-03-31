using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FrogScript : MonoBehaviour
{
    private Animator myAnim;

    private bool animation_started , animation_stopped , jumpLeft = true;
    private int jumpedTimes = 0;
    public LayerMask playerLayer;
    private GameObject player;

    private string coroutine_name = "FrogJump";

    private void Awake()
    {
        myAnim = GetComponent<Animator>();
    }

    void Start()
    {
        StartCoroutine(coroutine_name);
        player = GameObject.FindGameObjectWithTag(Mytags.PLAYER_TAG);
    }

    private void Update()
    {
        if(Physics2D.OverlapCircle(transform.position,0.5f,playerLayer))
        {
            player.GetComponent<PlayerDamage>().DealDamage();
        }
    }

    void LateUpdate()
    {
        if (animation_started && animation_stopped) {
            animation_started = false;
        
            transform.parent.position = transform.position;
     
            transform.localPosition = Vector3.zero;

         }
    }

    IEnumerator FrogJump()
    {
       yield return new WaitForSeconds(Random.Range(1f,4f));
         
        animation_started = true;
        animation_stopped = false;

        jumpedTimes++;
         
        if (jumpLeft)
        {
            myAnim.Play("FrogJumpLeft");
        }
        else
        {
            myAnim.Play("FrogJumpRight");
        }
        StartCoroutine(coroutine_name);
    }

    void AnimationFinished()
    {
        animation_stopped = true;
        if(jumpLeft)
        {
            myAnim.Play("FrogIdleLeft");
        }
        else
        {
            myAnim.Play("FrogIdleRight");
        }

        if(jumpedTimes == 3)
        {
            jumpedTimes = 0;

            Vector3 temp = transform.localScale;
            temp.x *= -1;
            transform.localScale = temp;

            jumpLeft = !jumpLeft;
        }
    }
}
