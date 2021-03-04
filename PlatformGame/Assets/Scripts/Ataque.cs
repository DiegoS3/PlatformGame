using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ataque : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("LeftWall"))
        {
            collision.gameObject.SetActive(false);
            Destroy(collision.gameObject, 0.5f);
            gameObject.SetActive(false);
            Destroy(gameObject, 0.1f);
            Debug.Log("Enemigo atacado");
        }
    }
}
