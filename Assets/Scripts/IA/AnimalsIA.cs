using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimalsIA : MonoBehaviour
{
    [SerializeField] private float velocidadMovimiento = 5f;  // Velocidad de movimiento
    [SerializeField] private Animator animator;
    [SerializeField] private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        // Inicia la rutina de movimiento
        StartCoroutine(MoverPorElMapa());
    }

    private IEnumerator MoverPorElMapa()
    {
        while (true)
        {
            // Calcula una nueva posición aleatoria en el mapa
            Vector3 randomPosition = GetRandomPositionOnMap();

            // Calcula la ruta hacia la nueva posición
            NavMeshPath path = new NavMeshPath();
            agent.CalculatePath(randomPosition, path);

            // Establece la ruta para la IA
            agent.SetPath(path);

            // Espera hasta que la IA llegue a la posición antes de calcular la siguiente ruta
            yield return new WaitUntil(() => agent.remainingDistance < 0.1f);

            // Pequeño tiempo de espera antes de calcular la siguiente ruta
            yield return new WaitForSeconds(1.0f);

            yield return null;
        }
    }

    private Vector3 GetRandomPositionOnMap()
    {
        // Genera una posición aleatoria dentro de un área grande del mapa
        Vector3 randomPosition = new Vector3(Random.Range(-50f, 50f), 0f, Random.Range(-50f, 50f));
        return randomPosition;
    }

    private void Update()
    {
        // Actualiza el parámetro de animación "Run" en función de si la IA está en movimiento
        animator.SetFloat("Run", agent.velocity.magnitude);
    }
}