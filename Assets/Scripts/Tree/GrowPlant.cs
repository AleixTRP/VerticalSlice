using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowPlant : MonoBehaviour
{
    [SerializeField] private GameObject[] spawnplants;
    private Map_Matrix mapMatrix;
    [SerializeField] private TreeScriptableObject Stree;

    private Animator animator;


    void Start()
    {
        mapMatrix = FindObjectOfType<Map_Matrix>();

        if (mapMatrix == null)
        {
            Debug.LogError("No se pudo encontrar un objeto Map_Matrix en la escena.");
        }
    }

    public void AdjustTreePosition(Vector3 plantPosition)
    {
        List<Vector3> plantPositions = GeneratePlantPositions(plantPosition);
        Vector3 averagePlantPosition = CalculateAveragePlantPosition(plantPositions);
        AdjustTreeHeight(averagePlantPosition);
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

            // Ajusta la altura de la posición para que esté en la superficie del terreno
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

    void AdjustTreeHeight(Vector3 averagePlantPosition)
    {
        // Ajusta la altura de la posición del árbol para que esté en la superficie del terreno
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

        // Establece la posición del árbol en el promedio de las posiciones de las plantas
        transform.position = new Vector3(averagePlantPosition.x, transform.position.y, averagePlantPosition.z);
    }

    public IEnumerator GrowTree()
    {
        float timeElapsed = 0f;
        Collider treeCollider = gameObject.GetComponent<Collider>();

        if (treeCollider != null)
        {
            treeCollider.isTrigger = false;
            gameObject.tag = "Untagged";
        }

        while (timeElapsed < Stree.growthSpeed)
        {
            
            gameObject.SetActive(true);
            timeElapsed += Time.deltaTime;
            gameObject.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, timeElapsed / Stree.growthSpeed);
            
            yield return null;
        }
    }
}

