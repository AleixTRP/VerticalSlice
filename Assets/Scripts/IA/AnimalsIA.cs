using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimalsIA : MonoBehaviour
{
    [SerializeField] private TreeScriptableObject Stree;  // Referencia al árbol
    [SerializeField] private float distanciaCircunferencia = 2f;  // Distancia desde el árbol
    [SerializeField] private float velocidadRotacion = 20f;  // Velocidad de rotación
    [SerializeField] private Animator animator;
    [SerializeField] private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        // Inicia la rotación alrededor del árbol
        StartCoroutine(RotarAlrededorArbol());
    }

    private IEnumerator RotarAlrededorArbol()
    {
        while (true)
        {
            // Calcula la nueva posición en la circunferencia alrededor del árbol
            float angle = Time.time * velocidadRotacion;
            Vector3 offset = new Vector3(Mathf.Cos(angle), 0f, Mathf.Sin(angle)) * distanciaCircunferencia;
            Vector3 targetPosition = Stree.treeTransform.position + offset;

            // Calcula la ruta hacia la nueva posición
            NavMeshPath path = new NavMeshPath();
            agent.CalculatePath(targetPosition, path);

            // Establece la ruta para la IA
            agent.SetPath(path);

            // Espera hasta que la IA llegue a la posición antes de calcular la siguiente ruta
            yield return new WaitUntil(() => agent.remainingDistance < 0.1f);

            // Pequeño tiempo de espera antes de calcular la siguiente ruta
            yield return new WaitForSeconds(0.5f);

            yield return null;
        }
    }

    private void Update()
    {
        // Actualiza el parámetro de animación "Run" en función de si la IA está en movimiento
        animator.SetFloat("Run", agent.velocity.magnitude);
    }
}