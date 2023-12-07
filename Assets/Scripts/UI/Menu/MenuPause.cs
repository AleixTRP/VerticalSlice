using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPause : MonoBehaviour
{
    public static bool GamePaused = false;
    [SerializeField] private GameObject pauseMenuUI;

    void Update()
    {
        if (Input_Manager._INPUT_MANAGER.GetEscButton())
        {
            
                Pause();
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

    public void QuitToMainMenu()
    {
        //Time.timeScale = 1f; // Asegúrate de restablecer el tiempo a su valor normal al cargar el menú principal
        SceneManager.LoadScene("InitialMenu");
    }
}