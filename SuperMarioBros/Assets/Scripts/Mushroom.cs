using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private bool moveLeft;
    [SerializeField] private AudioClip powerUpSound;

    private void Start()
    {
        moveLeft = false;
    }

    private void Update()
    {
        if(moveLeft)
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        else
            transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 7)
        {
            if(!moveLeft)
                moveLeft = true;
            else
                moveLeft = false;
        }

        if(collision.CompareTag("Player"))
        {   
            PlayerMovement.isGrow = true;
            Destroy(gameObject);
        }
    }
}
