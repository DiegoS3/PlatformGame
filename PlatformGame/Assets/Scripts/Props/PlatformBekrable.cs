using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBekrable : MonoBehaviour
{

    private Rigidbody2D rigidbody2D;
    public float fallDelay;
    public GameObject platform;
    private Vector3 totemPosOG;
    public float shakeMagnitude;

    // Start is called before the first frame update
    void Start()
    {
        totemPosOG = platform.transform.position;
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

  
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            InvokeRepeating("StartTotemShaking", 0f, 0.02f);
            StartCoroutine(PlatformFall());
        }
    }

    IEnumerator PlatformFall()
    {
        yield return new WaitForSeconds(fallDelay);
        StopTotemShaking();
        rigidbody2D.isKinematic = false;
        new WaitForSeconds(0.5f);
        GetComponent<Collider2D>().isTrigger = true;
        Destroy(gameObject, 0.6f);
        yield return 0;
    }

    
    private void StartTotemShaking()
    {
        platform.transform.position = totemPosOG;

        float random = UnityEngine.Random.value;
        float totemShakingOffSetX = random * shakeMagnitude * 2 - shakeMagnitude;

        Vector3 totemIntermediatePosition = platform.transform.position;

        totemIntermediatePosition.x += totemShakingOffSetX;

        platform.transform.position = totemIntermediatePosition;
    }

    private void StopTotemShaking()
    {
        CancelInvoke("StartTotemShaking");
        platform.transform.position = totemPosOG;
    }
}
