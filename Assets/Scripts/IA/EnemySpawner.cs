using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
   [SerializeField] private GameObject enemyPrefab;  // Asigna el prefab del enemigo desde el Inspector
   [SerializeField] private Transform spawnPoint;    // Asigna el punto de spawn desde el Inspector

    private DayNight dayNightScript;
    private bool hasSpawnedEnemy = false;

    private void Start()
    {
        dayNightScript = FindObjectOfType<DayNight>();  // Encuentra el script DayNight en la escena
    }

    private void Update()
    {
        // Verifica si es de noche (puedes ajustar el rango según tus necesidades)
        if (dayNightScript.GetCurrentHour() < 6 || dayNightScript.GetCurrentHour() > 18)
        {
            // Si no ha spawnado un enemigo, entonces lo spawneará
            if (!hasSpawnedEnemy)
            {
                SpawnEnemy();
                hasSpawnedEnemy = true;  // Marca que ya se spawnó un enemigo
            }
        }
        else
        {
            hasSpawnedEnemy = false;  // Reinicia el indicador cuando no es de noche
        }
    }

    private void SpawnEnemy()
    {
        // Spawnea el enemigo en el punto designado
        Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
    }
}