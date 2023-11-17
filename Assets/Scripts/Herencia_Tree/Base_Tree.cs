using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_Tree : MonoBehaviour
{
    [SerializeField] protected float growthTime = 10f;
    protected bool nearCuadrante = false;
    protected Map_Matrix mapMatrix;
    [SerializeField] protected GameObject Map_Terrain;
    [SerializeField] protected int WinPlants;
    [SerializeField] protected GameObject[] spawnplants;

    protected virtual void Start()
    {
        mapMatrix = FindObjectOfType<Map_Matrix>();
    }

    protected virtual void Update()
    {
        if (nearCuadrante && Input_Manager._INPUT_MANAGER.GetButtonPlant())
        {
            WinPlants++;
            Plant();
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Matrix"))
        {
            nearCuadrante = true;
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Matrix"))
        {
            nearCuadrante = false;
        }
    }

    protected virtual void Plant()
    {
        // Implementación común para plantar árboles
        Inventory playerInventory = GetComponent<Inventory>();

        if (playerInventory != null && playerInventory.InventoryCount > 0)
        {
            GameObject treeFromInventory = playerInventory.GetFirstTreeFromInventory();

            if (treeFromInventory != null)
            {
                Vector3 plantPosition = transform.position;
                mapMatrix.ActualizarCuadrante(plantPosition);

                AdjustTreePosition(treeFromInventory, plantPosition);

                StartCoroutine(GrowTree(treeFromInventory));

                playerInventory.RemoveFromInventory(treeFromInventory);

                Vector3 cuadrante = mapMatrix.ObtenerPosicionCuadrante(treeFromInventory.transform.position);
                Debug.Log("Árbol y plantas instanciados en el cuadrante: " + cuadrante);

                if (WinPlants == 3)
                {
                    Debug.LogError("Win");
                }
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

    protected virtual void AdjustTreePosition(GameObject tree, Vector3 plantPosition)
    {
        List<Vector3> plantPositions = GeneratePlantPositions(plantPosition);
        Vector3 averagePlantPosition = CalculateAveragePlantPosition(plantPositions);
        AdjustTreeHeight(tree, averagePlantPosition);
    }

    protected virtual List<Vector3> GeneratePlantPositions(Vector3 plantPosition)
    {
        List<Vector3> plantPositions = new List<Vector3>();

        for (int i = 0; i < spawnplants.Length; i++)
        {
            float angle = Random.Range(0f, 360f);
            float radius = Random.Range(2f, 4f);

            float plantSpawnX = (plantPosition.x + 1) + radius * Mathf.Cos(Mathf.Deg2Rad * angle);
            float plantSpawnZ = (plantPosition.z + 1) + radius * Mathf.Sin(Mathf.Deg2Rad * angle);

            Vector3 plantSpawnWorld = new Vector3(plantSpawnX + 1, 0f, plantSpawnZ + 1);

            float terrainHeight = mapMatrix.terreno.SampleHeight(plantSpawnWorld);
            float terrainY = mapMatrix.terreno.GetPosition().y;

            if (terrainHeight < terrainY)
            {
                plantSpawnWorld.y = terrainY;
            }
            else
            {
                plantSpawnWorld.y = terrainHeight;
            }

            Instantiate(spawnplants[i], plantSpawnWorld, Quaternion.identity);
            plantPositions.Add(plantSpawnWorld);
        }

        return plantPositions;
    }

    protected virtual Vector3 CalculateAveragePlantPosition(List<Vector3> plantPositions)
    {
        Vector3 averagePlantPosition = Vector3.zero;

        foreach (Vector3 plantPos in plantPositions)
        {
            averagePlantPosition += plantPos;
        }

        averagePlantPosition /= plantPositions.Count;

        return averagePlantPosition;
    }

    protected virtual void AdjustTreeHeight(GameObject tree, Vector3 averagePlantPosition)
    {
        float terrainHeightTree = mapMatrix.terreno.SampleHeight(averagePlantPosition);
        float terrainYTree = mapMatrix.terreno.GetPosition().y;

        if (terrainHeightTree < terrainYTree)
        {
            averagePlantPosition.y = terrainYTree;
        }
        else
        {
            averagePlantPosition.y = terrainHeightTree;
        }

        tree.transform.position = new Vector3(averagePlantPosition.x, tree.transform.position.y, averagePlantPosition.z);
    }

    protected virtual IEnumerator GrowTree(GameObject tree)
    {
        float timeElapsed = 0f;

        Collider treeCollider = tree.GetComponent<Collider>();
        if (treeCollider != null)
        {
            treeCollider.isTrigger = false;
            tree.tag = "Untagged";
        }

        while (timeElapsed < growthTime)
        {
            tree.SetActive(true);
            timeElapsed += Time.deltaTime;
            tree.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, timeElapsed / growthTime);
            yield return null;
        }
    }
}

