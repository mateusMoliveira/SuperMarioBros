using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallPlayer : MonoBehaviour
{
    //Metodo de morte para quando o jogador cair da cena
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if(PlayerMovement.isGrow)
                PlayerMovement.isGrow = false;
            else
            {
                FindObjectOfType<PlayerMovement>().Death();
            }
        }
    }
}
