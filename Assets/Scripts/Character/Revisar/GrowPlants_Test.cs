using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class GrowPlants_Test : MonoBehaviour
{
 
    private bool nearCuadrante = false;
    private Map_Matrix mapMatrix; // Referencia al script Map_Matrix

    [SerializeField]
    private GameObject Map_Terrain;


    void Start()
    {
        // Obt�n la referencia al script Map_Matrix
        mapMatrix = FindObjectOfType<Map_Matrix>();
    }

    void Update()
    {
        if (nearCuadrante && Input_Manager._INPUT_MANAGER.GetButtonPlant())
        {
           
            Plant();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Matrix")) // Cambia a la etiqueta correspondiente
        {
            nearCuadrante = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Matrix")) // Cambia a la etiqueta correspondiente
        {
            nearCuadrante = false;
        }
    }

    void Plant()
    {
        if (nearCuadrante)
        {
            Inventory playerInventory = GetComponent<Inventory>();

            if (playerInventory != null && playerInventory.InventoryCount > 0)
            {
                GameObject treeFromInventory = playerInventory.GetFirstTreeFromInventory();

                if (treeFromInventory != null)
                {
                    Vector3 plantPosition = transform.position;
                    mapMatrix.ActualizarCuadrante(plantPosition);

                    // Elimina el �rbol del inventario 
                    playerInventory.RemoveFromInventory(treeFromInventory);

                    // Obtiene el cuadrante despu�s de plantar el �rbol
                    Vector3 cuadrante = mapMatrix.ObtenerPosicionCuadrante(treeFromInventory.transform.position);
                    Debug.Log("�rbol y plantas instanciados en el cuadrante: " + cuadrante);

                   
                }
                else
                {
                    Debug.LogWarning("No se pudo obtener el �rbol del inventario.");
                }
            }
            else
            {
                Debug.LogWarning("Inventario no v�lido o sin �rboles.");
            }
        }
    }





    Vector3 CalculateAveragePlantPosition(List<Vector3> plantPositions)
    {
        Vector3 averagePlantPosition = Vector3.zero;

        foreach (Vector3 plantPos in plantPositions)
        {
            averagePlantPosition += plantPos;
        }

        averagePlantPosition /= plantPositions.Count;

        return averagePlantPosition;
    }
}