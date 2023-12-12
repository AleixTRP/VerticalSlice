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

        // Inicia la rutina de movimiento a lo largo del NavMesh
        StartCoroutine(MoverPorElNavMesh());
    }

    private IEnumerator MoverPorElNavMesh()
    {
        while (true)
        {
            // Calcula una nueva posición aleatoria en el NavMesh
            Vector3 randomPosition = GetRandomPositionOnNavMesh();

            // Establece la posición como destino para la IA
            agent.SetDestination(randomPosition);

            // Espera hasta que la IA llegue al destino antes de calcular la siguiente ruta
            yield return new WaitUntil(() => agent.remainingDistance < 0.1f);

            // Pequeño tiempo de espera antes de calcular la siguiente ruta
            yield return new WaitForSeconds(1.0f);
        }
    }

    private Vector3 GetRandomPositionOnNavMesh()
    {
        // Genera una posición aleatoria dentro del NavMesh
        NavMeshHit hit;
        Vector3 randomPosition = Vector3.zero;

        // Try to find a point on the NavMesh within a certain radius
        if (NavMesh.SamplePosition(transform.position + Random.onUnitSphere * 10f, out hit, 10f, NavMesh.AllAreas))
        {
            randomPosition = hit.position;
        }

        return randomPosition;
    }

    private void Update()
    {
        // Actualiza el parámetro de animación "Run" en función de si la IA está en movimiento
        animator.SetFloat("Run", agent.velocity.magnitude);
    }
}