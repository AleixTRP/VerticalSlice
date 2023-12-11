using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimalsIA : MonoBehaviour
{
    [SerializeField] private List<GameObject> puntosDestino = new List<GameObject>();
    [SerializeField] private float tiempoEspera = 10f;
    [SerializeField] private float tiempoEsperaSonido = 3f;
    [SerializeField] private NavMeshAgent agent;
    private GameObject puntoDestinoActual;
    private float tiempoActual = 0f;
    [SerializeField] private Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        // Asegúrate de tener puntos de destino en la lista
        if (puntosDestino.Count > 0)
        {
            // Inicia el movimiento hacia un punto de destino aleatorio
            SeleccionarNuevoDestinoAleatorio();
        }
        else
        {
            Debug.LogError("No hay puntos de destino configurados en la lista.");
        }
    }

    void Update()
    {
        // Si la IA llegó al destino, espera antes de moverse al siguiente
        if (agent.remainingDistance < 0.1f)
        {
            tiempoActual += Time.deltaTime;

            // Comprueba si es tiempo de detenerse y reproducir un sonido
            if (tiempoActual >= tiempoEspera)
            {
                tiempoActual = 0f;
               
            }
            // Si no es tiempo de detenerse, espera antes de seleccionar un nuevo destino
            else if (tiempoActual >= tiempoEsperaSonido)
            {
                tiempoActual = 0f; // Reinicia el temporizador antes de seleccionar el nuevo destino
                SeleccionarNuevoDestinoAleatorio();
            }
        }

        // Actualiza el parámetro de animación "Run" en función de si la IA está en movimiento
        animator.SetFloat("Run", agent.velocity.magnitude);

     
    }

    void SeleccionarNuevoDestinoAleatorio()
    {
        // Selecciona un punto de destino aleatorio de la lista
        puntoDestinoActual = puntosDestino[Random.Range(0, puntosDestino.Count)];

        // Establece el destino en el punto seleccionado
        agent.SetDestination(puntoDestinoActual.transform.position);

    }

}