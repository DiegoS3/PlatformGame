using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEnemy : MonoBehaviour
{
    public GameObject enemy;
    public Animator enemyAnim;
    public Transform player;
    public float agroRange;
    public float moveSpeed;
    public float moveSpeed_og;
    public Rigidbody2D rg2D;
    public Transform castPoint;
    public bool isEnemyLeft;
    private bool isAgro;
    private bool isSearching;

    private float waitTime;
    public Transform[] moveSpots;
    public float startWaitTime = 2;
    private int i = 0;
    private Vector2 actualPos;
    public GameObject hacha;


    // Start is called before the first frame update
    void Start()
    {
        moveSpeed_og = moveSpeed;
        rg2D = GetComponent<Rigidbody2D>();
        enemyAnim = enemy.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(CheckEnemyMoving());

        if (CanSeePlayer(agroRange))
        {
            isAgro = true;
        }
        else
        {
            if (isAgro)
            {
                if (!isSearching)
                {
                    isSearching = true;
                    Invoke("StopChasingPlayer", 5.0f);
                }
                
            }
        }
        if (isAgro)
        {
            ChasePlayer();
        }
        else
        {
            EnemyMove();
        }
    }

    private void EnemyMove()
    {
        transform.position = Vector2.MoveTowards(transform.position, moveSpots[i].transform.position, moveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, moveSpots[i].transform.position) < 0.2f)
        {
            if (waitTime <= 0 && !Espada.isDead)
            {
                if (moveSpots[i] != moveSpots[moveSpots.Length - 1])
                {
                    i++;  
                    transform.localScale = new Vector2(-1, 1);
                }
                else
                {
                    i = 0;
                    transform.localScale = new Vector2(1, 1);
                }

                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
                enemyAnim.SetBool("Idle", true);
            }
        }
    }

    private void StopChasingPlayer()
    {
        rg2D.velocity = new Vector2(0, 0);
        isAgro = false;
        isSearching = false;
        //CheckPosition();
    }

    private void CheckPosition()
    {
        if (isEnemyLeft)
        {
            transform.localScale = new Vector2(1, 1);
        }
        {
            transform.localScale = new Vector2(-1, 1);
        }
    }

    private void ChasePlayer()
    {
        if (transform.position.x < player.position.x)
        {
            //Enemigo esta a la izq del jugador, entonces se mueve a la derecha
            rg2D.velocity = new Vector2(moveSpeed, 0);
            transform.localScale = new Vector2(1,1);
            isEnemyLeft = false;
        }
        else
        {
            //Enemigo esta a la derecha del jugador, entonces se mueve a la izq
            rg2D.velocity = new Vector2(-moveSpeed, 0);
            transform.localScale = new Vector2(-1, 1);
            isEnemyLeft = true; 
        }
    }

    private bool CanSeePlayer(float distance)
    {
        bool val = false;
        float castDist = distance;

        if (isEnemyLeft)
        {
            castDist = -distance;
        }
        Vector2 endPos = castPoint.position + Vector3.right * castDist;
        RaycastHit2D hit = Physics2D.Linecast(castPoint.position, endPos, 1 << LayerMask.NameToLayer("Action"));

        if (hit.collider != null)
        {
            Debug.DrawLine(castPoint.position, hit.point, Color.red);

            if (hit.collider.CompareTag("Player"))
            {
                Invoke("Atacar", 0.5f);
                val = true;
            }
           
        }
        else
        {
            Debug.DrawLine(castPoint.position, endPos, Color.blue);
            enemyAnim.SetBool("Attack", false);
            moveSpeed = moveSpeed_og;
            hacha.SetActive(false);
        }

        return val;
    }

    private void Atacar()
    {
        enemyAnim.SetBool("Attack", true);
        moveSpeed = 0f;
        Invoke("hachaActiva",0.6f);
    }

    private void hachaActiva()
    {
        hacha.SetActive(true);
    }

    IEnumerator CheckEnemyMoving()
    {
        actualPos = transform.position;

        yield return new WaitForSeconds(0.05f);

        if (transform.position.x > actualPos.x)
        {
            transform.localScale = new Vector2(1, 1);
            isEnemyLeft = false;
            enemyAnim.SetBool("Idle", false);
            
        }
        else if (transform.position.x < actualPos.x)
        {
            transform.localScale = new Vector2(-1, 1);
            isEnemyLeft = true;
            enemyAnim.SetBool("Idle", false);
            
        }
        if (Espada.isDead)
        {
            moveSpeed = 0f;
            Debug.Log("Die");
            enemyAnim.SetBool("Die", true);
            new WaitForSeconds(0.5f);
            
        }

    }

}
