using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBekrable : MonoBehaviour
{

    private Rigidbody2D rigidbody2D;
    public float fallDelay;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {        

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(PlatformFall());
        }
    }

    IEnumerator PlatformFall()
    {
        yield return new WaitForSeconds(fallDelay);
        rigidbody2D.isKinematic = false;
        GetComponent<Collider2D>().isTrigger = true;
        yield return 0;
    }
}
