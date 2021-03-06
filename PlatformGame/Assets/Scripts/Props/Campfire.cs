using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Campfire : MonoBehaviour
{

    public ParticleSystem[] ps;
    public float fadeOutTime;
    private bool primera = false;


    private void fadeout()
    {
        if (!primera)
        {
            for (int i = 0; i < ps.Length; i++)
            {
                ps[i].Stop();
                Destroy(ps[i], fadeOutTime);
            }

            Personaje.vidas++;
            Debug.Log("Vida ++"+ Personaje.vidas.ToString());
            
            primera = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Invoke("fadeout", 1f);
        
    }


}
