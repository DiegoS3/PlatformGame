using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    public GameObject cloud;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        InvokeRepeating("MoveCloud", 0f, 0.02f);        
    }

    private void MoveCloud()
    {
        float random = Random.Range(0.0f, 0.002f);
        Vector3 cloudPosition = cloud.transform.position;
        cloudPosition.x -= random;
        cloud.transform.position = cloudPosition;
    }

    public void StopMoveCloud()
    {
        CancelInvoke("MoveCloud");
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
