using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InitialMenu : MonoBehaviour
{
    [SerializeField] private GameObject _camera;
    [SerializeField] private GameObject _menu;

    private void Start()
    {
        _menu.SetActive(true);
        Time.timeScale = 1f;

    }
    public void EjecutarAccionBoton()
    {
     
        if (Input_Manager._INPUT_MANAGER.GetLeftClick())
        {
            // Cambia a la escena especificada
            SceneManager.LoadScene("Lvl1");
        }
    }
}