using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGroundTotem : MonoBehaviour
{
    public static bool isGrounded;

    private void OnTriggerExit2D(Collider2D collision)
    {
        isGrounded = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

    }
}
