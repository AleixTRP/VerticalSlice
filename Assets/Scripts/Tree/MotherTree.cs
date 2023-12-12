using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MotherTree : MonoBehaviour
{
    [SerializeField] private float maxLife = 100f; // Vida máxima del árbol madre
    private float currentLife;   // Vida actual del árbol madre
    [SerializeField] private string lvls;
  

    private void Start()
    {
        currentLife = 0f; // Comienza con 0 de vida
    }

    public void AddLife(float lifeToAdd)
    {
        // Añadir vida al árbol madre
        currentLife += lifeToAdd;

        // Asegurarse de que la vida no sea mayor que la vida máxima
        currentLife = Mathf.Min(currentLife, maxLife);

        // Notificar a la UI sobre el cambio en la vida
        UpdateUI();

        // Verificar si la vida alcanza su máximo
        if (currentLife >= maxLife)
        {
            // Implementa la lógica de victoria del nivel
            Debug.Log("¡Has ganado el nivel!");

            SceneManager.LoadScene(lvls);
          

        }
    }

    private void UpdateUI()
    {
        // Llama a la actualización de la UI en el componente UI_MotherTree
        UI_MotherTree uiMotherTree = FindObjectOfType<UI_MotherTree>();
        if (uiMotherTree != null)
        {
            uiMotherTree.UpdateLifeUI(GetCurrentLife(), GetMaxLife());
        }
    }
    // Lógica para consumir vida del árbol madre si es necesario (por ejemplo, eventos negativos)
    public void ConsumeLife(float lifeToConsume)
    {
        // Restar vida al árbol madre
        currentLife -= lifeToConsume;

        // Asegurarse de que la vida no sea menor que 0
        currentLife = Mathf.Max(0, currentLife);

        // Actualizar la UI si es necesario
        UpdateUI();
    }



    public bool IsMotherTreeAlive()
    {
        return currentLife > 0;
    }

    public float GetCurrentLife()
    {
        return currentLife;
    }

    public float GetMaxLife()
    {
        return maxLife;
    }
}