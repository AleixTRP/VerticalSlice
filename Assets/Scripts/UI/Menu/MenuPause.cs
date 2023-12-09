using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPause : MonoBehaviour
{
    public static bool GamePaused = false;
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject optionMenu;

    void Update()
    {
        if (Input_Manager._INPUT_MANAGER.GetEscButton())
        {
            
                Pause();
            optionMenu.SetActive(false);
        }
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        //Time.timeScale = 0f; // Pausa el juego
        GamePaused = true;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        //Time.timeScale = 1f; // Reanuda el juego
        GamePaused = false;
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

    public void menuOptions()
    {
        optionMenu.SetActive(true);
    }

    public void optionsBack()
    {
        optionMenu.SetActive(false);
    }


}