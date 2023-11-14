using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class GrowPlants : MonoBehaviour
{
    [SerializeField]
    private float growthTime = 10f;

    private bool nearFlowerpot = false;
    private GameObject nearestFlowerpot;

    void Update()
    {
        if (nearFlowerpot && Input_Manager._INPUT_MANAGER.GetButtonPlant())
        {
            Plant();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Maceta"))
        {
            nearFlowerpot = true;
            nearestFlowerpot = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Maceta"))
        {
            nearFlowerpot = false;
            nearestFlowerpot = null;
        }
    }

    void Plant()
    {
        if (nearestFlowerpot != null)
        {
            Inventory playerInventory = GetComponent<Inventory>();

            if (playerInventory != null && playerInventory.InventoryCount > 0)
            {
                // Obtén el primer árbol del inventario
                GameObject treeFromInventory = playerInventory.GetFirstTreeFromInventory();

                if (treeFromInventory != null)
                {
                    // Mueve el árbol del inventario a la posición de la maceta y activa su GameObject
                    treeFromInventory.transform.position = nearestFlowerpot.transform.position;
                    treeFromInventory.SetActive(true);

                    // Desactiva el árbol como trigger
                    Collider treeCollider = treeFromInventory.GetComponent<Collider>();
                    if (treeCollider != null)
                    {
                        treeCollider.isTrigger = false;
                    }

                    // Quitar el tag del árbol
                    treeFromInventory.tag = "Untagged";

                    // Inicia la corutina de crecimiento para este árbol específico
                    StartCoroutine(GrowTree(treeFromInventory));

                    // Elimina el árbol del inventario (puedes hacerlo después de que haya crecido)
                    playerInventory.RemoveFromInventory(treeFromInventory);
                }
            }
        }
    }
    IEnumerator GrowTree(GameObject tree)
    {
        float timeElapsed = 0f;

        while (timeElapsed < growthTime)
        {
            timeElapsed += Time.deltaTime;
            tree.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, timeElapsed / growthTime);
            yield return null;
        }
    }
}