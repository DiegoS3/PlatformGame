using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public GameObject checkpoint;
    //public GameObject checkpointOff;
    public SpriteRenderer ckp;
    public Sprite ckpOn;
    private Vector3 posPj;

    // Start is called before the first frame update
    void Start()
    {
        posPj = FindObjectOfType<Personaje>().posRespawn;
        if (posPj == transform.position)
        {
            ckp.sprite = ckpOn;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            FindObjectOfType<Personaje>().posRespawn = new Vector3(transform.position.x +2, transform.position.y, transform.position.z);
            ckp.sprite = ckpOn;
            
        }
    }
}
