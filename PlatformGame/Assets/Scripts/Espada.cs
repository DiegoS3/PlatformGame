using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Espada : MonoBehaviour
{

    public static bool isDead;
    public float timeDead;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemigo"))
        {
            isDead = true;
            Destroy(collision.gameObject, 1.1f);
            Debug.Log("Enemigo atacado "+ isDead.ToString());
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isDead = false;
    }
}
