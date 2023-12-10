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
    private Audio_Manager audioManager;
    [SerializeField] private Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        audioManager = Audio_Manager.instance;
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
                DetenerYReproducirSonido();
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

        // Si la IA está quieta, reproduce el sonido
        if (agent.velocity.magnitude < 0.1f)
        {
            ReproducirSonidoCuandoQuieta();
        }
    }

    void DetenerYReproducirSonido()
    {
        // Detén la IA
        agent.ResetPath();

        // Reproduce el sonido utilizando el Audio_Manager
        if (audioManager != null)
        {
            audioManager.Play("DeerSound", transform.position);
        }
        else
        {
            Debug.LogError("No se encontró el AudioManager. Asegúrate de que esté configurado correctamente en tu escena.");
        }
    }

    void SeleccionarNuevoDestinoAleatorio()
    {
        // Selecciona un punto de destino aleatorio de la lista
        puntoDestinoActual = puntosDestino[Random.Range(0, puntosDestino.Count)];

        // Establece el destino en el punto seleccionado
        agent.SetDestination(puntoDestinoActual.transform.position);
       
    }

    void ReproducirSonidoCuandoQuieta()
    {
        // Reproduce el sonido cuando la IA está quieta
        if (audioManager != null)
        {
            // Obtén la posición actual del objeto
            Vector3 posicionActual = transform.position;

            // Si usas NavMeshAgent, obtén la posición proyectada en la superficie del NavMesh
            if (agent != null)
            {
                NavMeshHit hit;
                if (NavMesh.SamplePosition(posicionActual, out hit, 1.0f, NavMesh.AllAreas))
                {
                    posicionActual = hit.position;
                }
            }

            // Actualiza la posición del sonido en el Audio_Manager
            audioManager.UpdateSoundPosition("DeerSound", posicionActual);

            // Reproduce el sonido
            audioManager.Play("DeerSound", posicionActual);
        }
        else
        {
            Debug.LogError("No se encontró el AudioManager. Asegúrate de que esté configurado correctamente en tu escena.");
        }
    }
}