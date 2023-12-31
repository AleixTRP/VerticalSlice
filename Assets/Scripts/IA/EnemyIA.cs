using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyIA : MonoBehaviour
{
    private MotherTree motherTree;
    private float moveSpeed = 5f;
    private float damageToMotherTree = 1f;  // Definir la variable para el da�o al �rbol madre
    private float attackDistance = 2f;      // Definir la variable para la distancia de ataque
    [SerializeField] private Animator animator;
    [SerializeField] private NavMeshAgent agent;

    void Start()
    {
        motherTree = FindObjectOfType<MotherTree>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (motherTree != null)
        {
            // Mueve hacia la posici�n del �rbol madre
            MoveTowardsMotherTree();

            // Ataca al �rbol madre si est� lo suficientemente cerca
            AttackMotherTree();
        }
    }

    void MoveTowardsMotherTree()
    {
        Vector3 direction = (motherTree.transform.position - transform.position).normalized;
        transform.Translate(direction * moveSpeed * Time.deltaTime);
        animator.SetFloat("Run", agent.velocity.magnitude);
    }

    void AttackMotherTree()
    {
        float distanceToMotherTree = Vector3.Distance(transform.position, motherTree.transform.position);

        if (distanceToMotherTree < attackDistance)
        {
            // Imprime un mensaje de prueba para verificar que esta parte se est� ejecutando
            Debug.Log("Atacando al �rbol madre");

            // Implementa la l�gica de ataque
            motherTree.ConsumeLife(damageToMotherTree * Time.deltaTime);
        }
    }
}