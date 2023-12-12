using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
    public static bool GamePaused = false;

    private void Start()
    {
        Audio_Manager.instance.Play("Win");
    }
    public void NextLvl()
    {
        SceneManager.LoadScene("Lvl1");
    }

    public void quitToMainMenu()
    {
        GamePaused = false;
        //Time.timeScale = 1f; // Asegúrate de restablecer el tiempo a su valor normal al cargar el menú principal
        SceneManager.LoadScene("InitialMenu");
    }

    public void exitGame()
    {
        Application.Quit();
    }


}