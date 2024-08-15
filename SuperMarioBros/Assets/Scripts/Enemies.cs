using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    public GameObject pointA, pointB;
    private Rigidbody2D rb;
    private Animator animGoomba;
    private BoxCollider2D colliderGoomba;
    private Transform currentPoint;
    private AudioSource audioKick;

    [SerializeField] private float speed;
    [SerializeField] private AudioClip kickSound;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animGoomba = GetComponent<Animator>();
        colliderGoomba = GetComponent<BoxCollider2D>();
        currentPoint = pointB.transform;
        audioKick = GetComponent<AudioSource>();
    }

    //Move o inimigo de um ponto A ao ponto B, invertendo suas sprites
    private void Update()
    {
        if(currentPoint == pointB.transform)
            rb.velocity = new Vector2(speed, 0);
        else
            rb.velocity = new Vector2(-speed, 0);
        
        if(Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointB.transform)
        {
            Flip();
            currentPoint = pointA.transform;
        }
        if(Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointA.transform)
        {
            Flip();
            currentPoint = pointB.transform;
        }
    }
    
    //Metodo para inverter os sprites no eixo x
    private void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    //Detecta a colisao entre jogador e inimigo e decide quem ira morrer
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if(transform.position.y + 0.5f < collision.transform.position.y)
            {   
                audioKick.clip = kickSound;
                audioKick.Play();
                collision.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                collision.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 6, ForceMode2D.Impulse);
                animGoomba.SetTrigger("death");
                speed = 0;
                Destroy(gameObject, 0.3f);
                colliderGoomba.enabled = false;
            }
            else
            {
                if(PlayerMovement.isGrow)
                    PlayerMovement.isGrow = false;
                else
                {
                    FindObjectOfType<PlayerMovement>().Death();

                    Enemies[] goomba = FindObjectsOfType<Enemies>();

                    for(int i = 0; i < goomba.Length; i++)
                    {
                        goomba[i].speed = 0;
                        goomba[i].animGoomba.speed = 0;
                    }
                }
            }
        }
    }
}
