using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{

    private float posX, posY;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetFloat("posX") != 0)
        {
            transform.position = new Vector2(PlayerPrefs.GetFloat("posX"), PlayerPrefs.GetFloat("posY"));
        }
    }

    public void ReachedCheckPoint(float x, float y)
    {
        PlayerPrefs.SetFloat("posX", x);
        PlayerPrefs.SetFloat("posY", y);
    }
}
