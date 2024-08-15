using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{
    [SerializeField] private string nameMap;

    //Troca de cena e inicia o jogo
    public void Play()
    {
        SceneManager.LoadScene(nameMap);
    }

    //Fecha o jogo
    public void Quit()
    {
        Application.Quit();
    }
}
