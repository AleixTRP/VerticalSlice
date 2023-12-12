using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inventory : MonoBehaviour
{
    [SerializeField]
    private TMP_Text inventoryText;

    void Update()
    {
        // Actualizar el Texto de la UI con información del inventario
        inventoryText.text =  Inventory.Instance.InventoryCount.ToString();
    }
}