using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_Matrix : MonoBehaviour
{
    
    private int filas = 100;
    
    private int columnas = 100;

    [SerializeField]
    private Terrain terreno;
   
    [SerializeField]
    private Transform jugador; 

    private bool[,] cuadrantesReplantados;

    private Vector3 posicionAnteriorJugador; 

    void Start()
    {
        if (terreno == null || jugador == null)
        {
            Debug.LogError("Referencias no asignadas en el Inspector.");
            return;
        }

        InicializarMatrizCuadrantes();
        DividirTerrenoEnCuadrantes();

       
        posicionAnteriorJugador = jugador.position;
    }

    void Update()
    {
        // Verifica cambios en el cuadrante cada vez que la posición del jugador cambia
        if (jugador.position != posicionAnteriorJugador)
        {
            VerificarCambioCuadrante();
            // Actualiza la posición anterior del jugador
            posicionAnteriorJugador = jugador.position;
        }
    }

    void VerificarCambioCuadrante()
    {
        // Calcula las coordenadas del cuadrante actual del jugador
        int filaActual = Mathf.FloorToInt(jugador.position.z / terreno.terrainData.size.z * filas);
        int columnaActual = Mathf.FloorToInt(jugador.position.x / terreno.terrainData.size.x * columnas);



        // Muestra un mensaje de debug si el jugador cambia de cuadrante
        Debug.Log("El jugador ha cambiado de cuadrante. Fila: " + filaActual + ", Columna: " + columnaActual);
    }
    void InicializarMatrizCuadrantes()
    {
        cuadrantesReplantados = new bool[filas, columnas];
       
    }

    void DividirTerrenoEnCuadrantes()
    {
        // Obtener el tamaño total del terreno
        float tamanoX = terreno.terrainData.size.x;
        float tamanoZ = terreno.terrainData.size.z;

        // Calcular el tamaño de cada cuadrante
        float tamanoCuadranteX = tamanoX / columnas;
        float tamanoCuadranteZ = tamanoZ / filas;

        // Iterar a través de las filas y columnas para crear los cuadrantes
        for (int fila = 0; fila < filas; fila++)
        {
            for (int columna = 0; columna < columnas; columna++)
            {
                // Calcular las coordenadas del cuadrante
                float x = columna * tamanoCuadranteX;
                float z = fila * tamanoCuadranteZ;

                
                cuadrantesReplantados[fila, columna] = true;
            }
        }

    
    }
}