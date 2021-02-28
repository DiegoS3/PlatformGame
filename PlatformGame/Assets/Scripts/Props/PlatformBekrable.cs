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
    private float shakeTime;
    public float startShakeTime;

    // Start is called before the first frame update
    void Start()
    {
        totemPosOG = platform.transform.position;
        shakeTime = startShakeTime;
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (shakeTime <= 0)
        {            
            shakeTime = startShakeTime;
            while(shakeTime > 0)
            {
                InvokeRepeating("StartTotemShaking", 0f, 0.02f);
                shakeTime -= Time.deltaTime;
            }
            shakeTime = startShakeTime;            
        }
        else
        {
            //StopTotemShaking();
            shakeTime -= Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StopTotemShaking();
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
