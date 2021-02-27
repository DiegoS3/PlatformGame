using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personaje : MonoBehaviour
{

    public float runSpeed = 2;
    public float jumpSpeed = 3;

    public bool betterJump = false;
    public float fallMultiplier = 0.5f;
    public float lowJumpMultiplier = 1f;

    Rigidbody2D rb2D;
    public SpriteRenderer spriteRenderer;
    public Animator animator;

    public float walljumpforce;
    public Vector2 walljumpAngle;

    bool isTouchingFront = false;
    bool wallSliding;
    public float wallSlidingSpeed = 0.75f;
    bool isTouchingRight;
    bool isTouchingLeft;




    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

        if (isTouchingFront && !CheckGround.isGrounded)
        {
            wallSliding = true;
        }
        else
        {
            wallSliding = false;
        }

        if (wallSliding)
        {
            animator.Play("PjSlide");
            rb2D.velocity = new Vector2(rb2D.velocity.x, Mathf.Clamp(rb2D.velocity.y, -wallSlidingSpeed, float.MaxValue));

        }
        if (!CheckGround.isGrounded)
        {
            animator.SetBool("Jump", true);
            animator.SetBool("Run", false);
            //animator.Play("PjFall");

        }

        if (CheckGround.isGrounded)
        {
            animator.SetBool("Jump", false);
        }



    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey("d") || Input.GetKey("right") && !isTouchingRight)
        {
            rb2D.velocity = new Vector2(runSpeed, rb2D.velocity.y);
            spriteRenderer.flipX = false;
            animator.SetBool("Run", true);
        }
        else if (Input.GetKey("a") || Input.GetKey("left") && !isTouchingLeft)
        {
            rb2D.velocity = new Vector2(-runSpeed, rb2D.velocity.y);
            spriteRenderer.flipX = true;
            animator.SetBool("Run", true);
        }
        else
        {
            rb2D.velocity = new Vector2(0, rb2D.velocity.y);
            animator.SetBool("Run", false);
        }
        if (Input.GetKey("w") && CheckGround.isGrounded && !wallSliding)
        {
            
            rb2D.velocity = new Vector2(rb2D.velocity.x, jumpSpeed);
            
            
        }

        if (Input.GetKey("w") && wallSliding)
        {
            rb2D.velocity = new Vector3(-rb2D.velocity.x, jumpSpeed, rb2D.velocity.y);
        }

       

        if (betterJump && !wallSliding)
        {
            if (rb2D.velocity.y < 0)
            {
                rb2D.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier) * Time.deltaTime;
            }
            if (rb2D.velocity.y > 0 && !Input.GetKey("w"))
            {
                rb2D.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier) * Time.deltaTime;
            }
        }

        if (Input.GetKey("space"))
        {
            animator.SetBool("Attack", true);
            animator.SetBool("Run", false);
        }
        else
        {
            animator.SetBool("Attack", false);
        }

        if (isTouchingFront && !CheckGround.isGrounded)
        {
            animator.SetBool("Falling", true);
        }
        else
        {
            animator.SetBool("Falling", false);
        }

    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("RigthWall"))
        {
            isTouchingFront = true;
            isTouchingRight = true;
        }
        if (collision.gameObject.CompareTag("LeftWall"))
        {
            isTouchingFront = true;
            isTouchingLeft = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isTouchingFront = false;
        isTouchingLeft = false;
        isTouchingRight = false;
    }

}
