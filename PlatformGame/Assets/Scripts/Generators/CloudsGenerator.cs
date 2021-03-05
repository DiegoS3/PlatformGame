using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudsGenerator : MonoBehaviour
{
    public GameObject[] clouds;
    public float probGenerate;
    public float probCloudType;
    public float waitSecond;
    private GameObject cloud;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RandomGenerateCloud();        

    }

    private void RandomGenerateCloud()
    {
        new WaitForSeconds(waitSecond);
        float random = Random.Range(0.0f, 100.0f);
        float randomSize = Random.Range(0.4f, 1f);       

        if (random < probGenerate)
        {
            if (random > probCloudType)
            {
                cloud = clouds[1];
            }
            else
            {
                cloud = clouds[0];
            }

            cloud.transform.localScale = new Vector2(randomSize, randomSize);

            GameObject.Instantiate(cloud, transform.position, transform.rotation);
        }    
    }

}
