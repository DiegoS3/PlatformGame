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
        if (PlayerPrefs.HasKey("x"))
        {
            posPj = new Vector3(PlayerPrefs.GetFloat("x")-2, PlayerPrefs.GetFloat("y"), PlayerPrefs.GetFloat("z"));
        }
        if (posPj == transform.position)
        {
            ckp.sprite = ckpOn;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //FindObjectOfType<Personaje>().posRespawn = new Vector3(transform.position.x +2, transform.position.y, transform.position.z);
            PlayerPrefs.SetFloat("x", transform.position.x + 2);
            PlayerPrefs.SetFloat("y", transform.position.y);
            PlayerPrefs.SetFloat("z", transform.position.z);
            ckp.sprite = ckpOn;
            
        }
    }
}
