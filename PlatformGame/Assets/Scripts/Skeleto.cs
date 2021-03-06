using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleto : MonoBehaviour
{

    //public Animator animator;
    public float distanceRaycast;
    private float coolDownAttack = 1.5f;
    private float actualCoolDownAttack;
    public GameObject hacha;



    private void Start()
    {
        hacha.SetActive(false);
        actualCoolDownAttack = 0;
        hacha.SetActive(false);
    }

    private void Update()
    {
        actualCoolDownAttack -= Time.deltaTime;
        if (AISkeleton.right)
        {
            Debug.DrawRay(transform.position, Vector2.right, Color.red, distanceRaycast);
        }
        else
        {
            Debug.DrawRay(transform.position, Vector2.left, Color.green, distanceRaycast);
        }
        
    }

    private void FixedUpdate()
    {
        RaycastHit2D hit2D;

        if (AISkeleton.right)
        {
            hit2D = Physics2D.Raycast(transform.position, Vector2.right, distanceRaycast);
        }
        else
        {
            hit2D = Physics2D.Raycast(transform.position, Vector2.left, distanceRaycast);
        }

             

        if (hit2D.collider != null)
        {
            if (hit2D.collider.CompareTag("Player"))
            {
                Debug.Log("Player dado con rayCast");
                if (actualCoolDownAttack < 0)
                {
                    Invoke("hachaActive", 0.5f);
                    //animator.Play("Attack");
                    actualCoolDownAttack = coolDownAttack;
                    hacha.SetActive(false);
                }
            }
        }
    }

    private void hachaActive()
    {
        hacha.SetActive(true);
    }

}
