using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudsDestroyer : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cloud"))
        {
            collision.gameObject.SetActive(false);
            Destroy(collision.gameObject, 0.2f);
        }
    }
}
