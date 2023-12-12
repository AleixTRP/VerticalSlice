using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimalsIA : MonoBehaviour
{
    [SerializeField] private float velocidadMovimiento = 5f;  // Velocidad de movimiento
    [SerializeField] private Animator animator;
    [SerializeField] private NavMeshAgent agent;

    private GameObject enemy;

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
            // Busca el GameObject del enemigo en la escena
            enemy = GameObject.FindGameObjectWithTag("Enemy");

            if (enemy != null)
            {
                // Mueve hacia la posici�n del enemigo y lo sigue
                MoveTowardsEnemy();
            }

            // Peque�o tiempo de espera antes de calcular la siguiente ruta
            yield return new WaitForSeconds(1.0f);
        }
    }

    private void MoveTowardsEnemy()
    {
        // Si hay un enemigo, mueve hacia �l y lo sigue
        agent.SetDestination(enemy.transform.position);

        // Actualiza el par�metro de animaci�n "Run" en funci�n de si la IA est� en movimiento
        animator.SetFloat("Run", agent.velocity.magnitude);
    }
}