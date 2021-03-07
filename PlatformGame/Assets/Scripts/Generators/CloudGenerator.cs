using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudGenerator : MonoBehaviour
{
    public GameObject[] clouds;
    public float spawnInterval;
    public GameObject endPoint;
    private Vector3 startPos;
    public int a;


    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        Invoke("AttemptSpawn", spawnInterval);
        PreWarm();
    }

    private void SpawnCloud(Vector3 startPos)
    {
        int rand = Random.Range(0, clouds.Length);
        a = rand;
        GameObject cloud = Instantiate(clouds[rand]);

        float startY = Random.Range(startPos.y - 2f, startPos.y +1f);
        cloud.transform.position = new Vector3(startPos.x, startY, startPos.z);

        float scala = Random.Range(0.6f, 1.2f);
        cloud.transform.localScale = new Vector2(scala, scala);

        float speed = Random.Range(0.3f, 1.5f);
        cloud.GetComponent<Cloud>().StartFloating(speed, endPoint.transform.position.x);
    }

    private void AttemptSpawn()
    {
        SpawnCloud(startPos);
        Invoke("AttemptSpawn", spawnInterval);
    }

    private void PreWarm()
    {
        for (int i = 0; i < 3; i++)
        {
            Vector3 spawnPos = startPos + Vector3.right * (i - 5 * 3);
            SpawnCloud(spawnPos);
        }
    }
}
