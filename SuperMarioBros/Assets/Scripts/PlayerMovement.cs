using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private float horizontalInput;
    private Rigidbody2D rb;
    private bool inFloor;
    private Animator animMario;
    private SpriteRenderer spriteRenderer;
    private CapsuleCollider2D colliderPlayer;
    public static bool isGrow, verifyIsGrow;

    private int movendoHash = Animator.StringToHash("moving");
    private int saltandoHash = Animator.StringToHash("jumping");

    [SerializeField] private float speed, jump;
    [SerializeField] private Transform peDoPersonagem;
    [SerializeField] private LayerMask layer;
    [SerializeField] private bool dead = false;
    [SerializeField] private string nameMap, nextMap;
    [SerializeField] private GameObject checkPoint;
    [SerializeField] private AudioClip downSound;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip powerUpSound;
    
    private AudioSource audioPowerUp;
    private AudioSource audioDown;
    private AudioSource audioJump;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animMario = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        colliderPlayer = GetComponent<CapsuleCollider2D>();
        audioDown = GetComponent<AudioSource>();
        audioJump = GetComponent<AudioSource>();
        audioPowerUp = GetComponent<AudioSource>();
    }

    private void Start()
    {
        dead = false;
        isGrow = false;
        verifyIsGrow = false;
    }

    private void Update()
    {
        //Verificacao para reiniciar a cena caso o jogador esteja morto
        if(dead) return;

        Moving();
        //Verificacao para pular e soltar o audio de pulo do jogador
        if(JumpPlayer())
        {
            rb.AddForce(Vector2.up * jump);
            audioJump.clip = jumpSound;
            audioJump.Play();
        }

        //Verificacao para soltar o audio de quando o jogador cresce
        if(verifyIsGrow != isGrow && isGrow == true)
        {
            audioPowerUp.clip = powerUpSound;
            audioPowerUp.Play();
            verifyIsGrow = true;
        }
        else if(verifyIsGrow != isGrow && isGrow == false)
        {
            verifyIsGrow = false;
        }

        inFloor = Physics2D.OverlapCircle(peDoPersonagem.position, 0.2f, layer);

        animMario.SetBool(movendoHash, horizontalInput != 0);
        animMario.SetBool(saltandoHash, !inFloor);
        animMario.SetBool("grow", isGrow);

        //Verificacao para inverter o sprite do jogador
        if(horizontalInput > 0)
            spriteRenderer.flipX = false;
        else if(horizontalInput < 0)
            spriteRenderer.flipX = true;
    }

    //Metodo responsavel por mover o jogador de acordo com as teclas pressionadas
    private void Moving()
    {
        if(dead) return;

        horizontalInput = Input.GetAxis("Horizontal");
    }

    //Metodo de verificacao para o pulo do personagem
    private bool JumpPlayer()
    {
        if(Input.GetKeyDown(KeyCode.Space) && inFloor)
        {
            return true;
        }
        else
            return false; 
    }

    private void FixedUpdate()
    {
        if(dead) return;

        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
    }

    //Verificacoes para soltar audio e animacao de morte do jogador caso tenha morrido
    public void Death()
    {
        StartCoroutine(DeathCorotine());
    }

    IEnumerator DeathCorotine()
    {
        if(!dead)
        {
            dead = true;
            animMario.SetTrigger("death");
            audioDown.clip = downSound;
            audioDown.Play();
            yield return new WaitForSeconds(0.5f);
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * 10f, ForceMode2D.Impulse);
            colliderPlayer.isTrigger = true;
            Invoke("RestartGame", 2.5f);
        }
    }
    
    //Metodo responsavel por reiniciar a cena
    private void RestartGame()
    {
        SceneManager.LoadScene(nameMap);
    } 

    //Metodo responsavel por ir para a proxima cena
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("checkPoint"))
            SceneManager.LoadScene(nextMap);
    }
}

    
