using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class MisteryBox : MonoBehaviour
{
    [SerializeField] private float spawnHeight = 0.8f;
   
    [SerializeField] private GameObject spawnObject;
    [SerializeField]private AudioClip puAppearSound;

    private Animator animBox;
    private AudioSource audiopuAppear;
    private void Awake()
    {
        animBox = GetComponent<Animator>();
        audiopuAppear = GetComponent<AudioSource>();
    }

    //Detecta a colisao entre jogador e caixa e cria um cogumelo na cena
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")  && this.enabled)
        {
            this.enabled = false;
            animBox.SetTrigger("hit");
            Instantiate(spawnObject, new Vector2(transform.position.x,transform.position.y + spawnHeight),Quaternion.identity);
            audiopuAppear.clip = puAppearSound;
            audiopuAppear.Play();
        }
    }
}
