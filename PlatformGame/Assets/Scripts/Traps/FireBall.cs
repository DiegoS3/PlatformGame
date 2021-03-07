using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{

    public float speed;
    public Transform[] patrols;
    private int i = 0;
    public GameObject fireball;
    private Collider2D col;

    // Start is called before the first frame update
    void Start()
    {
        col = transform.gameObject.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, patrols[i].transform.position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, patrols[i].transform.position) < 0.1f)
        {

            if (patrols[i] != patrols[patrols.Length - 1])
            {
                i++;
                fireball.transform.localScale = new Vector3(1, -1, 1);
                
            }
            else
            {
                fireball.transform.localScale = new Vector3(1, 1, 1);
                i = 0;
            }

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            col.enabled = false;
            Debug.Log("FireBall pegango");
            Personaje.vidas--;
            Debug.Log(Personaje.vidas);
        }
        
    }
}
