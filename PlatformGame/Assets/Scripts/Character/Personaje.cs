using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Personaje : MonoBehaviour
{

    public static int vidas = 5;
    public static int monedas;
    bool isdead = false;

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

    public GameObject espada;
    public GameObject ataque_original;
    public GameObject ataque_posicion;

    public bool atacando;
    public float tiempoAtaque = 0.5f;
    public bool puedeSaltar;
    public float tiempoSalto = 0.5f;

    public GameObject spawnInit;

    public float timeInAir = 1.7f;
    public Vector3 posRespawn;


    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("x"))
        {
            Vector3 newPos = new Vector3(PlayerPrefs.GetFloat("x"), PlayerPrefs.GetFloat("y"), PlayerPrefs.GetFloat("z"));
            transform.position = newPos;
        }
        else
        {
            posRespawn = spawnInit.transform.position;
            transform.position = posRespawn;
        }

        monedas = 0;
        puedeSaltar = true;
        atacando = false;
        espada.SetActive(false);
        Debug.Log("Daño: " + vidas);
        rb2D = GetComponent<Rigidbody2D>();
        animator.SetBool("Fall", false);
        animator.SetBool("Jump", false);
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

        if (vidas <= 0)
        {
            isdead = true;
        }

        if (isdead)
        {
            animator.SetBool("Dead", true);
            runSpeed = 0;
            Invoke("ReSpawnPlayer", 2f);
            //ReSpawnPlayer();
            //resetPlayer();

        }

        dmgFall();
    }

    private void dmgFall()
    {
        //Here we decrease the time in air from 5
        //Grounded is used by Rigidbody controller
        if (!CheckGround.isGrounded && !wallSliding)
        {
            timeInAir -= Time.deltaTime;
        }

        //Increase the time in air to reach 5 each time we're on ground
        if ((CheckGround.isGrounded || wallSliding) && timeInAir < 1.7f && timeInAir > 0)
        {
            timeInAir = 1.7f;
        }

        //Making the player die when it reaches 0
        if (timeInAir <= 0 && CheckGround.isGrounded)
        {
            isdead = true;
            Debug.Log("You Died!" + isdead);
        }

        //Or just damage player on a "checkpoint", in this case I use 3
        if (timeInAir == 1f && CheckGround.isGrounded)
        {
            vidas--;
        }
    }


    private void resetPlayer()
    {
        timeInAir = 1.7f;
        runSpeed = 4f;
        vidas = 5;
        monedas = 0;
        animator.SetBool("Dead", false);
        
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
            if (puedeSaltar)
            {
                rb2D.velocity = new Vector2(rb2D.velocity.x, jumpSpeed);
                puedeSaltar = false;
                Invoke("dejarDeSaltar", tiempoSalto);
            }
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
                espada.SetActive(true);
                Invoke("dejarDeAtacar", tiempoAtaque);
        }
        else
        {
            
        }

        if (isTouchingFront && !CheckGround.isGrounded)
        {
            animator.SetBool("Falling", true);
        }
        else
        {
            animator.SetBool("Falling", false);
        }

        //No permitir al character moverse mientras este la anima de damage
        if (!this.animator.GetCurrentAnimatorStateInfo(0).IsName("PjDano"))
        {
            runSpeed = 4;
        }

    }

    private void ReSpawnPlayer()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //transform.position = new Vector3(posRespawn.x, posRespawn.y, posRespawn.z);
        isdead = false;
        resetPlayer();

    }


    private void dejarDeSaltar()
    {
        puedeSaltar = true;
    }

    private void dejarDeAtacar()
    {
        atacando = false;
        animator.SetBool("Attack", false);
        espada.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap") || collision.gameObject.CompareTag("Enemigo"))
        {
            vidas--;
            runSpeed = 0;
            Debug.Log("Daño: " + vidas);
            animDanoPj();
        }

        if (collision.gameObject.CompareTag("Death"))
        {
            vidas = 0;
            Debug.Log("Daño mortal: " + vidas);
            isdead = true;
        }

    }

    private void animDanoPj()
    {
        animator.Play("PjDano");

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
