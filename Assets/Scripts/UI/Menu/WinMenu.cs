using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
    private static bool GamePaused = false;

    // Nombre de la escena a cargar, puedes establecerlo desde el Inspector
    [SerializeField] private string nextSceneName = "Lvl2";



    public void NextLvl()
    {
        // Cargar la escena utilizando el nombre almacenado en la variable nextSceneName
        SceneManager.LoadScene(nextSceneName);
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