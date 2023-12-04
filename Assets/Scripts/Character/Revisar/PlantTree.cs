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

        // Obtén la referencia al script Map_Matrix
        mapMatrix = FindObjectOfType<Map_Matrix>();
    }

    void Update()
    {
        if (nearCuadrante)
        {
            //var tree = Instantiate
            Debug.Log("Entra en la animación de plantar");
       

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

                    // Inicia la corutina de crecimiento para este árbol específico
                    StartCoroutine(treeFromInventory.GrowTree());

                    // Elimina el árbol del inventario 
                    playerInventory.RemoveFromInventory(treeFromInventory.gameObject);

                    // Obtiene el cuadrante después de plantar el árbol
                    Vector3 cuadrante = mapMatrix.ObtenerPosicionCuadrante(treeFromInventory.transform.position);
                    Debug.Log("Árbol y plantas instanciados en el cuadrante: " + cuadrante);
                }
                else
                {
                    Debug.LogWarning("No se pudo obtener el árbol del inventario.");
                }
            }
            else
            {
                Debug.LogWarning("Inventario no válido o sin árboles.");
            }
        }
    }
}

 