using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemFall : MonoBehaviour
{

    public float speed;
    private float waitTime;
    public Transform[] patrols;
    private float startWaitTime;
    private int i = 0;
    private bool suelo;


    // Start is called before the first frame update
    void Start()
    {
        waitTime = startWaitTime;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, patrols[i].transform.position, speed * Time.deltaTime);

        if (!suelo)
        {
            startWaitTime = 10f;
        }
        else
        {
            startWaitTime = 1f;
        }

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            suelo = true;
            Debug.Log("SUELO");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        suelo = false;
    }
}
