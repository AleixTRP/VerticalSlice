using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class GrowPlants : MonoBehaviour
{
    public GameObject treePrefab;  // Assign the tree 3D model in the Inspector.
    public float growthTime = 10f;

    private bool nearFlowerpot = false;
    private GameObject nearestFlowerpot; // Reference to the nearest flowerpot object.

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
            // Instantiate the tree at the nearest flowerpot's position.
            GameObject newTree = Instantiate(treePrefab, nearestFlowerpot.transform.position, Quaternion.identity);
            StartCoroutine(GrowTree(newTree));
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