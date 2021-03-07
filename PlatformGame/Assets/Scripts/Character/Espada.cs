using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Espada : MonoBehaviour
{

    public static bool isDead;
    public float timeDead;

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
