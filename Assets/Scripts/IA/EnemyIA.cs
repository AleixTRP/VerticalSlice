using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIA : MonoBehaviour
{
    private MotherTree motherTree;
    private float moveSpeed = 5f;
    private float damageToMotherTree = 1f;  // Definir la variable para el daño al árbol madre
    private float attackDistance = 2f;      // Definir la variable para la distancia de ataque

    void Start()
    {
        motherTree = FindObjectOfType<MotherTree>();
    }

    void Update()
    {
        if (motherTree != null)
        {
            // Mueve hacia la posición del árbol madre
            MoveTowardsMotherTree();

            // Ataca al árbol madre si está lo suficientemente cerca
            AttackMotherTree();
        }
    }

    void MoveTowardsMotherTree()
    {
        Vector3 direction = (motherTree.transform.position - transform.position).normalized;
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }

    void AttackMotherTree()
    {
        float distanceToMotherTree = Vector3.Distance(transform.position, motherTree.transform.position);

        if (distanceToMotherTree < attackDistance)
        {
            // Imprime un mensaje de prueba para verificar que esta parte se está ejecutando
            Debug.Log("Atacando al árbol madre");

            // Implementa la lógica de ataque
            motherTree.ConsumeLife(damageToMotherTree * Time.deltaTime);
        }
    }
}