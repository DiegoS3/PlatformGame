using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public GameObject checkpointOn;
    public GameObject checkpointOff;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.DeleteAll();
        if (PlayerPrefs.GetFloat("posX") != 0)
        {
            if (checkpointOff.transform.position.x == PlayerPrefs.GetFloat("posX"))
            {
                Instantiate(checkpointOn, transform.position, transform.rotation);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerRespawn>().ReachedCheckPoint(transform.position.x, transform.position.y);
            //checkpointOn.transform.localScale = checkpointOff.transform.localScale;
            //Instantiate(checkpointOn, transform.position, transform.rotation);
            //DestroyImmediate(checkpointOff, true);
        }
    }
}
