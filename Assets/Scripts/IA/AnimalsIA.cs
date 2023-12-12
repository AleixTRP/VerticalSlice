using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimalsIA : MonoBehaviour
{
    [SerializeField] private TreeScriptableObject Stree;  // Referencia al �rbol
    [SerializeField] private float distanciaCircunferencia = 2f;  // Distancia desde el �rbol
    [SerializeField] private float velocidadRotacion = 20f;  // Velocidad de rotaci�n
    [SerializeField] private Animator animator;
    [SerializeField] private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        // Inicia la rotaci�n alrededor del �rbol
        StartCoroutine(RotarAlrededorArbol());
    }

    private IEnumerator RotarAlrededorArbol()
    {
        while (true)
        {
            // Calcula la nueva posici�n en la circunferencia alrededor del �rbol
            float angle = Time.time * velocidadRotacion;
            Vector3 offset = new Vector3(Mathf.Cos(angle), 0f, Mathf.Sin(angle)) * distanciaCircunferencia;
            Vector3 targetPosition = Stree.treeTransform.position + offset;

            // Calcula la ruta hacia la nueva posici�n
            NavMeshPath path = new NavMeshPath();
            agent.CalculatePath(targetPosition, path);

            // Establece la ruta para la IA
            agent.SetPath(path);

            // Espera hasta que la IA llegue a la posici�n antes de calcular la siguiente ruta
            yield return new WaitUntil(() => agent.remainingDistance < 0.1f);

            // Peque�o tiempo de espera antes de calcular la siguiente ruta
            yield return new WaitForSeconds(0.5f);

            yield return null;
        }
    }

    private void Update()
    {
        // Actualiza el par�metro de animaci�n "Run" en funci�n de si la IA est� en movimiento
        animator.SetFloat("Run", agent.velocity.magnitude);
    }
}