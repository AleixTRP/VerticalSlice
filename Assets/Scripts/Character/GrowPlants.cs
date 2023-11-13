using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowPlants : MonoBehaviour
{
    public GameObject treePrefab;
    public Transform potTransform;
    public float growthPerSecond = 0.1f;
    public float maxSize = 1.0f;
    public float growthTime = 5.0f; // Time in seconds before reaching maximum size
    public Color finalColor = Color.green; // Color to be applied when the tree reaches its maximum size

    private GameObject currentTree;
    private float elapsedTime;

    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= growthTime)
        {
            if (currentTree != null && currentTree.transform.localScale.x < maxSize)
            {
                GrowTree();
            }
            else
            {
                ChangeTreeColor();
            }
        }

        if (Input_Manager._INPUT_MANAGER.GetButtonPlant())
        {
            PlantTree();
        }
    }

    void PlantTree()
    {
        if (currentTree == null)
        {
            currentTree = Instantiate(treePrefab, potTransform.position, Quaternion.identity);
            currentTree.transform.parent = potTransform;
            elapsedTime = 0f; // Reset the timer when planting a new tree
        }
    }

    void GrowTree()
    {
        currentTree.transform.localScale += new Vector3(growthPerSecond * Time.deltaTime,
                                                        growthPerSecond * Time.deltaTime,
                                                        growthPerSecond * Time.deltaTime);
    }

    void ChangeTreeColor()
    {
        Renderer treeRenderer = currentTree.GetComponent<Renderer>();
        if (treeRenderer != null)
        {
            treeRenderer.material.color = finalColor;
        }
    }
}