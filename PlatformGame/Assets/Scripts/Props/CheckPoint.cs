using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public GameObject checkpoint;
    //public GameObject checkpointOff;
    public SpriteRenderer ckp;
    public Sprite ckpOn;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.DeleteAll();
        if (PlayerPrefs.GetFloat("posX") != 0)
        {
            if (checkpoint.transform.position.x == PlayerPrefs.GetFloat("posX"))
            {
                //Instantiate(checkpointOn, transform.position, transform.rotation);
                //ckp.size = new Vector3(0.62343f, 0.62343f, 0.62343f);
                ckp.sprite = ckpOn;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //collision.GetComponent<PlayerRespawn>().ReachedCheckPoint(transform.position.x, transform.position.y);
            ckp.sprite = ckpOn;
            //checkpointOn.transform.localScale = checkpointOff.transform.localScale;
            //Instantiate(checkpointOn, transform.position, transform.rotation);
            //DestroyImmediate(checkpointOff, true);
        }
    }
}
