using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformUpDown : MonoBehaviour
{

    private PlatformEffector2D effector2D;
    public float startWaitTime;
    private float waitedTime;

    // Start is called before the first frame update
    void Start()
    {
        effector2D = GetComponent<PlatformEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp("s"))
        {
            waitedTime = startWaitTime;
        }

        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey("s"))
        {
            if (waitedTime <= 0)
            {
                effector2D.rotationalOffset = 180f;
                waitedTime = startWaitTime;
            }
            else
            {
                waitedTime -= Time.deltaTime;
            }
        }

        if (Input.GetKey("w"))
        {
            effector2D.rotationalOffset = 0;
        }
    }
}
