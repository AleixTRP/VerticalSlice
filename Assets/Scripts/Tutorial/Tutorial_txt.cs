using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_txt : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject[] objetosFlotantes;
    [SerializeField] private float tiempoEntreObjetos = 2f; // Tiempo en segundos entre objetos

    private int currentIndex = 0;
    private bool todosObjetosMostrados = false;

    private void Start()
    {
        StartCoroutine(CambiarObjetoConDelay());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RangoCharacter") && !todosObjetosMostrados)
        {
            MostrarObjeto();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("RangoCharacter"))
        {
            OcultarObjeto();
        }
    }

    private void MostrarObjeto()
    {
        Audio_Manager.instance.Play("Page");
        canvas.SetActive(true);
        StartCoroutine(CambiarObjetoConDelay());
    }

    private void OcultarObjeto()
    {
        canvas.SetActive(false);
    }

    private IEnumerator CambiarObjetoConDelay()
    {
        // Espera el tiempo especificado antes de cambiar al próximo objeto
        yield return new WaitForSeconds(tiempoEntreObjetos);

        // Desactiva el objeto actual
        objetosFlotantes[currentIndex].SetActive(false);
        Audio_Manager.instance.Play("Page");
        // Cambia al próximo objeto
        currentIndex++;

        // Verifica si todos los objetos han sido mostrados
        if (currentIndex >= objetosFlotantes.Length)
        {
            todosObjetosMostrados = true;
            OcultarObjeto(); // Si ya se mostraron todos, oculta el objeto
        }
        else
        {
            // Activa el próximo objeto
            objetosFlotantes[currentIndex].SetActive(true);

            // Reinicia el proceso para cambiar el objeto
            StartCoroutine(CambiarObjetoConDelay());
        }
    }
}