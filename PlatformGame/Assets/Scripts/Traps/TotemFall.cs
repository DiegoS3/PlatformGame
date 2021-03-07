using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemFall : MonoBehaviour
{

    private float speed;
    private float waitTime;
    public Transform[] patrols;
    private float startWaitTime;
    private int i = 0;
    private bool moving;
    public float waitOnGround;
    public float waitOnAir;
    public float speedOnGround;
    public float speedOnAir;
    public GameObject totem;
    private Vector3 totemPosOG;
    public float shakeMagnitude;
    public float shakeTime;
    public Collider2D lineDeath;


    // Start is called before the first frame update
    void Start()
    {
        totemPosOG = totem.transform.position;
        if (patrols[i] == patrols[patrols.Length - 1])
        {
            startWaitTime = waitOnAir;
            speed = speedOnGround;
        }
        else
        {
            if (!moving)
            {
                InvokeRepeating("StartTotemShaking", 0f, 0.05f);
                Invoke("StopTotemShaking", shakeTime);
            }
            startWaitTime = waitOnGround;
            speed = speedOnAir;
        }
        waitTime = startWaitTime;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, patrols[i].transform.position, speed * Time.deltaTime);        

        if (Vector2.Distance(transform.position, patrols[i].transform.position) < 0.1f)
        {
            if (waitTime <= 0)
            {
                moving = true;
                if (patrols[i] != patrols[patrols.Length - 1])
                {
                    i++;
                }
                else
                {
                    i = 0;
                }
                lineDeath.enabled = false;
                waitTime = startWaitTime;
            }
            else
            {
                moving = false;
                lineDeath.enabled = true;
                waitTime -= Time.deltaTime;

            }
        }

        if (patrols[i] == patrols[patrols.Length - 1])
        {
            startWaitTime = waitOnAir;
            speed = speedOnGround;
        }
        else
        {
            if (!moving)
            {
                InvokeRepeating("StartTotemShaking", 0f, 0.05f);
                Invoke("StopTotemShaking", shakeTime);
            }
            startWaitTime = waitOnGround;
            speed = speedOnAir;
        }
    }

    private void StartTotemShaking()
    {
        totem.transform.position = totemPosOG;

        float random = Random.value;
        float totemShakingOffSetX = random * shakeMagnitude * 2 - shakeMagnitude;

        Vector3 totemIntermediatePosition = totem.transform.position;

        totemIntermediatePosition.x += totemShakingOffSetX;

        totem.transform.position = totemIntermediatePosition;
    }

    private void StopTotemShaking()
    {
        CancelInvoke("StartTotemShaking");
        totem.transform.position = totemPosOG;
    }
}
