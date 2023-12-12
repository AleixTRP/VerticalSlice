using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GrowPlant : MonoBehaviour
{
    [SerializeField] private GameObject[] spawnplants;
    private Map_Matrix mapMatrix;
    [SerializeField] private TreeScriptableObject Stree;
    private DayNight dayNight;
    private float multiplayerDay;
    private GameObject spawnedAnimal;  // Nueva variable para almacenar la instancia del animal

    void Start()
    {
        mapMatrix = FindObjectOfType<Map_Matrix>();
        dayNight = FindObjectOfType<DayNight>();
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

        transform.position = new Vector3(averagePlantPosition.x, transform.position.y, averagePlantPosition.z);
    }

    public IEnumerator GrowTree()
    {
        gameObject.SetActive(true);
        float timeElapsed = 0f;
        Collider treeCollider = gameObject.GetComponent<Collider>();

        if (treeCollider != null)
        {
            treeCollider.isTrigger = false;
            gameObject.tag = "Untagged";
        }

        while (timeElapsed < Stree.growthSpeed)
        {
            float timeday = dayNight.GetCurrentHour();

            if (timeday >= 6 && timeday < 19)
            {
                multiplayerDay = Stree.dayMultiplier;
            }
            else
            {
                multiplayerDay = Stree.nightMultiplier;
            }

            timeElapsed += Time.deltaTime * multiplayerDay;
            gameObject.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, timeElapsed / Stree.growthSpeed);

            yield return null;
        }

        // The tree is fully grown, so add life to the MotherTree
        MotherTree motherTree = FindObjectOfType<MotherTree>();
        if (motherTree != null)
        {
            motherTree.AddLife(Stree.lifeToMotherTree);

            // Llamada para actualizar la UI
            UI_MotherTree uiMotherTree = FindObjectOfType<UI_MotherTree>();
            if (uiMotherTree != null)
            {
                uiMotherTree.UpdateLifeUI(motherTree.GetCurrentLife(), motherTree.GetMaxLife());
            }
        }

        SpawnAnimal();
        Audio_Manager.instance.Play("TreeReady");
    }

    void SpawnAnimal()
    {
        if (gameObject != null && Stree.animalPrefab != null)
        {
            Vector3 animalSpawnPosition = gameObject.transform.position + new Vector3(2f, 0f, 0f);
            spawnedAnimal = Instantiate(Stree.animalPrefab, animalSpawnPosition, Quaternion.identity);
        }
    }
}