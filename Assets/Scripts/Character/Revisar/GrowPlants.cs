using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class GrowPlants : MonoBehaviour
{
    [SerializeField] private float growthTime = 10f;
    private bool nearCuadrante = false;
    private Map_Matrix mapMatrix; // Referencia al script Map_Matrix

    [SerializeField]
    private GameObject Map_Terrain;

    [SerializeField] private int WinPlants;
    [SerializeField] private GameObject[] spawnplants;
    [SerializeField] private Animator animator;

    void Start()
    {
        // Obt�n la referencia al script Map_Matrix
        mapMatrix = FindObjectOfType<Map_Matrix>();
    }

    void Update()
    {
        if (nearCuadrante && Input_Manager._INPUT_MANAGER.GetButtonPlant())
        {
            animator.SetBool("plant", true);
            Debug.Log("Entra en la animaci�n de plantar");
            WinPlants++;
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

                    // Ajusta la posici�n del �rbol en funci�n de las plantas peque�as
                    AdjustTreePosition(treeFromInventory, plantPosition);
                   
                    // Inicia la corutina de crecimiento para este �rbol espec�fico
                    StartCoroutine(GrowTree(treeFromInventory));

                    // Elimina el �rbol del inventario 
                    playerInventory.RemoveFromInventory(treeFromInventory);

                    // Obtiene el cuadrante despu�s de plantar el �rbol
                    Vector3 cuadrante = mapMatrix.ObtenerPosicionCuadrante(treeFromInventory.transform.position);
                    Debug.Log("�rbol y plantas instanciados en el cuadrante: " + cuadrante);

                    if (WinPlants == 3)
                    {
                        Debug.LogError("Win");
                    }
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

    void AdjustTreePosition(GameObject tree, Vector3 plantPosition)
    {
        // Ajusta la posici�n del �rbol en funci�n de las plantas peque�as
        List<Vector3> plantPositions = GeneratePlantPositions(plantPosition);

        Vector3 averagePlantPosition = CalculateAveragePlantPosition(plantPositions);

        AdjustTreeHeight(tree, averagePlantPosition);
    }

    List<Vector3> GeneratePlantPositions(Vector3 plantPosition)
    {
        List<Vector3> plantPositions = new List<Vector3>();

        for (int i = 0; i < spawnplants.Length; i++)
        {
            float angle = Random.Range(0f, 360f);
            float radius = Random.Range(2f, 4f);

            float plantSpawnX = (plantPosition.x + 1) + radius * Mathf.Cos(Mathf.Deg2Rad * angle);
            float plantSpawnZ = (plantPosition.z + 1) + radius * Mathf.Sin(Mathf.Deg2Rad * angle);

            Vector3 plantSpawnWorld = new Vector3(plantSpawnX + 1, 0f, plantSpawnZ + 1);

            // Ajusta la altura de la posici�n para que est� en la superficie del terreno
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

    void AdjustTreeHeight(GameObject tree, Vector3 averagePlantPosition)
    {
        // Ajusta la altura de la posici�n del �rbol para que est� en la superficie del terreno
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

        // Establece la posici�n del �rbol en el promedio de las posiciones de las plantas
        tree.transform.position = new Vector3(averagePlantPosition.x, tree.transform.position.y, averagePlantPosition.z);
    }

    IEnumerator GrowTree(GameObject tree)
    {
        float timeElapsed = 0f;
        animator.SetBool("plant", false);
        // Desactiva el trigger antes de comenzar el crecimiento
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