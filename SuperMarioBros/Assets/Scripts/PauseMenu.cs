using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private string nameMap;
    [SerializeField] private AudioClip saveSound;

    private AudioSource audioSave;
    public static bool gameIsPause = false;

    private void Start()
    {
        audioSave = GetComponent<AudioSource>();
    }

    //Metodo responsavel por abir o Menu ao apertar ESC
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(gameIsPause)
                OpenMenu();
            else
                CloseMenu();
        }
    }

    //Metodo responsavel por abrir e configura o Menu
    public void OpenMenu()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPause = false;

    }

    //Metodo responsavel por fechar o Menu
    public void CloseMenu()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPause = true;
    }

    //Metodo responsavel por continuar o jogo de onde o usuario parou
    public void Continue()
    {   
        audioSave.clip = saveSound;
        audioSave.Play();
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPause = true;
    }

    //Metodo responsavel por voltar ao Menu Principal
    public void BackToMenu()
    {   
        audioSave.clip = saveSound;
        audioSave.Play();
        Time.timeScale = 1f;
        SceneManager.LoadScene(nameMap);
    }
}
