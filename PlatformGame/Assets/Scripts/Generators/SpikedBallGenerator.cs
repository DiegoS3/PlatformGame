using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikedBallGenerator : MonoBehaviour
{

    public GameObject spikedBall;
    public float probGenerate;
    public float waitSecond;


    // Update is called once per frame
    void Update()
    {
        RandomGenerateSpikedBall();
    }

    private void RandomGenerateSpikedBall()
    {
        new WaitForSeconds(waitSecond);
        float random = UnityEngine.Random.Range(0.0f, 100.0f);

        if (random < probGenerate)
        {
            GameObject.Instantiate(spikedBall, transform.position, transform.rotation);
        }
               
    }
}
