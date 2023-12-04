using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlantTree : MonoBehaviour
{
    private bool nearCuadrante = false;
    private Map_Matrix mapMatrix; // Referencia al script Map_Matrix

    [SerializeField] private GameObject Map_Terrain;
  
    [SerializeField] private int WinPlants;

    void Start()
    {

        // Obt�n la referencia al script Map_Matrix
        mapMatrix = FindObjectOfType<Map_Matrix>();
    }

    void Update()
    {
        if (nearCuadrante)
        {
            //var tree = Instantiate
            Debug.Log("Entra en la animaci�n de plantar");
       

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

    public void Plant()
    {
        if (nearCuadrante)
        {
            Inventory playerInventory = GetComponent<Inventory>();

            if (playerInventory != null && playerInventory.InventoryCount > 0)
            {
                GrowPlant treeFromInventory = playerInventory.GetFirstTreeFromInventory().GetComponent<GrowPlant>();


                if (treeFromInventory != null)
                {
                    Vector3 plantPosition = transform.position;
                    mapMatrix.ActualizarCuadrante(plantPosition);

                    treeFromInventory.AdjustTreePosition(plantPosition);

                    // Inicia la corutina de crecimiento para este �rbol espec�fico
                    StartCoroutine(treeFromInventory.GrowTree());

                    // Elimina el �rbol del inventario 
                    playerInventory.RemoveFromInventory(treeFromInventory.gameObject);

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
}

 