using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikedBallGenerator : MonoBehaviour
{

    public GameObject spikedBall;
    public float probGenerate;
    private int spikedBallCount = 0;
    public float waitSecond;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //StartCoroutine(RandomGenerateSpikedBall());
        RandomGenerateSpikedBall();
    }

    private void RandomGenerateSpikedBall()
    {

        //while(spikedBallCount < 5)
        //{
            new WaitForSeconds(waitSecond);
            float random = UnityEngine.Random.Range(0.0f, 100.0f);

            if (random < probGenerate)
            {
                GameObject.Instantiate(spikedBall, transform.position, transform.rotation);
                //yield return new WaitForSeconds(0.0001f);
                spikedBallCount++;
            }
        //}        
    }
}
