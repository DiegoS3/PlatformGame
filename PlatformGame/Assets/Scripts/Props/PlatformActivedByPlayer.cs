using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformActivedByPlayer : MonoBehaviour
{
    public float speed;
    private float waitTime;
    public Transform[] patrols;
    public float startWaitTime;
    private int i = 0;
    public bool puedeMoverse;


    // Start is called before the first frame update
    void Start()
    {
        waitTime = startWaitTime;
        puedeMoverse = false;
    }

    private void Update()
    {
        if (puedeMoverse)
        {
            transform.position = Vector2.MoveTowards(transform.position, patrols[i].transform.position, speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, patrols[i].transform.position) < 0.1f)
            {
                if (waitTime <= 0)
                {
                    if (patrols[i] != patrols[patrols.Length - 1])
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
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("juagdor");
            collision.collider.transform.SetParent(transform);
            puedeMoverse = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.collider.transform.SetParent(null);
    }
}
