using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oak_Tree : Base_Tree
{
    [SerializeField] private float oakGrowthTime = 40f;  // Nuevo tiempo de crecimiento para el roble
    [SerializeField] private GameObject oakPrefab;       // Nuevo prefab para el roble

    protected override void Start()
    {
        base.Start();  // Llamada al m�todo Start de la clase base

        // Puedes agregar cualquier inicializaci�n espec�fica del roble aqu� si es necesario.
    }

    protected override void Plant()
    {
        Inventory playerInventory = GetComponent<Inventory>();

        if (playerInventory != null && playerInventory.InventoryCount > 0)
        {
            GameObject treeFromInventory = playerInventory.GetFirstTreeFromInventory();

            if (treeFromInventory != null)
            {
                // Verifica si el �rbol en el inventario es un roble
                if (treeFromInventory.GetComponent<Oak_Tree>() != null)
                {
                    Vector3 plantPosition = transform.position;
                    mapMatrix.ActualizarCuadrante(plantPosition);

                    AdjustTreePosition(treeFromInventory, plantPosition);

                    StartCoroutine(GrowOakTree(treeFromInventory));

                    playerInventory.RemoveFromInventory(treeFromInventory);

                    Vector3 cuadrante = mapMatrix.ObtenerPosicionCuadrante(treeFromInventory.transform.position);
                    Debug.Log("Roble y plantas instanciados en el cuadrante: " + cuadrante);

                    if (WinPlants == 3)
                    {
                        Debug.LogError("Win");
                    }
                }
                else
                {
                    Debug.LogWarning("No se pudo obtener el roble del inventario.");
                }
            }
            else
            {
                Debug.LogWarning("Inventario no v�lido o sin robles.");
            }
        }
        else
        {
            Debug.LogWarning("Inventario no v�lido o sin �rboles.");
        }
    }

    // Nuevo m�todo espec�fico para el roble.
    protected virtual IEnumerator GrowOakTree(GameObject tree)
    {
        float timeElapsed = 0f;

        Collider treeCollider = tree.GetComponent<Collider>();
        if (treeCollider != null)
        {
            treeCollider.isTrigger = false;
            tree.tag = "Untagged";
        }

        // Asegur�monos de que el �rbol base est� desactivado durante el crecimiento del roble.
        tree.SetActive(false);

        // Genera la posici�n para el roble.
        Vector3 oakPosition = transform.position;
        mapMatrix.ActualizarCuadrante(oakPosition);

        // Ajusta la posici�n del roble.
        AdjustTreePosition(tree, oakPosition);

        // Obtiene las posiciones para las plantas alrededor del roble.
        List<Vector3> plantPositions = GeneratePlantPositions(oakPosition);

        while (timeElapsed < oakGrowthTime)
        {
            timeElapsed += Time.deltaTime;

            // Si ya se ha activado el roble, evita instanciarlo de nuevo.
            if (!tree.activeSelf)
            {
                tree.SetActive(true);
            }

            tree.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, timeElapsed / oakGrowthTime);
            yield return null;
        }

        // Instancia las plantas alrededor del roble despu�s de que ha crecido completamente.
        foreach (Vector3 plantPosition in plantPositions)
        {
            Instantiate(spawnplants[Random.Range(0, spawnplants.Length)], plantPosition, Quaternion.identity);
        }
    }
}