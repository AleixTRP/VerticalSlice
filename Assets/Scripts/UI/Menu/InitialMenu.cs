using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InitialMenu : MonoBehaviour
{
    [SerializeField] private GameObject _menu;

    private void Start()
    {
        _menu.SetActive(true);
      
    }

 
    public void EjecutarAccionBoton()
    {
        // Cambia a la escena especificada
       
        SceneManager.LoadScene("Lvl2");
       


    }
}

