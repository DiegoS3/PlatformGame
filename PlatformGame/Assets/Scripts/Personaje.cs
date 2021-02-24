using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personaje : MonoBehaviour
{

    private bool saltando;
    private bool atacando;
    private int vidas;
    private int monedas;

    public float salto;
    public GameObject atqOG;
    public GameObject atqPosition;

    // Start is called before the first frame update
    void Start()
    {
        saltando = false;
        atacando = false;
        vidas = 3;
        monedas = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(new Vector3(-0.1f, 0.0f));
        }
        else
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(new Vector3(0.1f, 0.0f));
        }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (!saltando)
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0.0f, salto));
                saltando = true;
            }            
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!atacando)
            {
                GameObject.Instantiate(atqOG, atqPosition.transform.position, atqPosition.transform.rotation);
                atacando = true;
                Invoke("TerminarAtaque", 0.5f);
            }            
        }

    }

    private void TerminarAtaque()
    {
        atacando = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Suelo"))
        {
            saltando = false;
        }

        if (collision.gameObject.tag.Equals("Enemigo"))
        {           
            collision.gameObject.SetActive(false);
            Destroy(collision.gameObject, 0.5f);
            vidas--;
            Debug.Log("Vidas " + vidas);

            if (vidas <= 0)
            {
                Debug.Log("El jugador ha muerto");
                gameObject.SetActive(false);

                int recorUltimo = PlayerPrefs.GetInt("Monedas");
                if (!PlayerPrefs.HasKey("Monedas"))
                {
                    //No hay record guardado
                    PlayerPrefs.SetInt("Monedas", monedas);
                    //TODO: poner aqui una pantalla de nuevo record
                }
                else
                {
                    //Si hay record guardado
                    if (recorUltimo < monedas)
                    {
                        //Nuevo record
                        PlayerPrefs.SetInt("Monedas", monedas);
                    }
                }
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Moneda"))
        {
            collision.gameObject.SetActive(false);
            Destroy(collision.gameObject, 0.5f);
            monedas++;
            Debug.Log("Monedas " + monedas);
        }
    }

}
