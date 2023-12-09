using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InitialMenu : MonoBehaviour
{
    [SerializeField] private GameObject _menu;
    [SerializeField] private GameObject _menuOptions;
   
    [SerializeField] private GameObject closeButton;
    [SerializeField] private GameObject optionsButton;

    private void Start()
    {
        _menu.SetActive(true);
      
    }

 
    public void EjecutarAccionBoton()
    {
        // Cambia a la escena especificada
       
        SceneManager.LoadScene("Lvl2");
       


    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void initalOptions()
    {
        _menuOptions.SetActive(true);
        closeButton.SetActive(false);
        optionsButton.SetActive(false);
    }

    public void BackOptions()
    {
        _menuOptions.SetActive(false);
        closeButton.SetActive(true);
        optionsButton.SetActive(true);
    }


}

