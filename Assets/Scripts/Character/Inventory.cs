using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<GameObject> inventory = new List<GameObject>();

    [SerializeField]
    private int maxInventorySize = 10; // Tama�o m�ximo del inventario



    public static Inventory Instance;

    public int InventoryCount
    {
        get { return inventory.Count; }
    }


    private void Awake()
    {

        // Configurar la referencia est�tica al inventario
        Instance = this;
    }


    public void AddToInventory(GameObject item)
    {
        if (!IsInventoryFull)
        {
            inventory.Add(item);

            // Desactiva el objeto en el suelo al agregarlo al inventario
            item.SetActive(false);



            // Mensaje de depuraci�n
            Debug.Log("A�adido al inventario: " + item.name);
        }
        else
        {
            Debug.LogWarning("El inventario est� lleno. No se puede agregar m�s elementos.");
        }
    }

    public void UseItem(int index)
    {
        if (index >= 0 && index < inventory.Count)
        {
            // Activa el objeto para que sea visible e interactivo
            inventory[index].SetActive(true);

            // Mensaje de depuraci�n
            Debug.Log("Usando elemento del inventario: " + inventory[index].name);


            // Elimina el objeto del inventario
            inventory.RemoveAt(index);
        }
    }

    public bool IsInventoryFull
    {
        get { return inventory.Count >= maxInventorySize; }
    }

    public GameObject GetFirstTreeFromInventory()
    {
        if (inventory.Count > 0)
        {

            return inventory[0];

        }
        else
        {
            return null;
        }
    }

    public void RemoveFromInventory(GameObject item)
    {
        inventory.Remove(item);
    }

    private void OnTriggerEnter(Collider other)
    {

        // Verifica si el objeto en el suelo es interactuable y si el inventario no est� lleno
        if (other.CompareTag("SmallPine") && !IsInventoryFull)
        {
            // Agrega el objeto al inventario cuando el jugador pasa por encima
            AddToInventory(other.gameObject);
        }
    }

}