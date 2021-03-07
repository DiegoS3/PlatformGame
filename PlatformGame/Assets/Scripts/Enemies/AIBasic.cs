using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBasic : MonoBehaviour
{
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public float speed = 0.5f;
    private float waitTime;
    public Transform[] moveSpots;
    public float startWaitTime = 2;
    private int i = 0;
    private Vector2 actualPos;
    public bool isSke = false;
    public static bool right = true;
    private Collider2D col;
    private bool isDead;

    void Start()
    {
        waitTime = startWaitTime;
        col = transform.gameObject.GetComponent<Collider2D>();

    }

    void Update()
    {
        StartCoroutine(CheckEnemyMoving());

        transform.position = Vector2.MoveTowards(transform.position, moveSpots[i].transform.position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, moveSpots[i].transform.position) < 0.2f)
        {
            if (waitTime <= 0 && !isDead)
            {
                if (moveSpots[i] != moveSpots[moveSpots.Length-1])
                {
                    i++;
                }
                else
                {
                    i = 0;
                }

                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }

    IEnumerator CheckEnemyMoving()
    {
        actualPos = transform.position;

        yield return new WaitForSeconds(0.05f);
        
        if (transform.position.x > actualPos.x)
        {
            if (isSke)
            {
                spriteRenderer.flipX = false;
                right = true;
            }
            else
            {
                spriteRenderer.flipX = true;
                
            }
            
            animator.SetBool("Idle", false);
        }
        else if (transform.position.x < actualPos.x)
        {
            if (isSke)
            {
                spriteRenderer.flipX = true;
                right = false;
            }
            else
            {
                spriteRenderer.flipX = false;
            }
            
            animator.SetBool("Idle", false);
        }
        else if (transform.position.x == actualPos.x)
        {
            animator.SetBool("Idle", true);
        }
        if (isDead)
        {
            speed = 0;
            col.enabled = false;
            Debug.Log("Die");
            //new WaitForSeconds(0.3f);
            animator.SetBool("Die", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Espada"))
        {
            isDead = true;
        }

    }
}
