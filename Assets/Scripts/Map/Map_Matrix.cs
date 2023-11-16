using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_Matrix : MonoBehaviour
{
    
    private int filas = 100;
    
    private int columnas = 100;

    private int subdivisionesEnFilas = 2;
    private int subdivisionesEnColumnas = 2;

   
    public Terrain terreno;
   
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

        // Inicializa la posición anterior del jugador al inicio
        posicionAnteriorJugador = ObtenerPosicionCuadrante(jugador.position);
    }

    void Update()
    {
        Vector3 nuevaPosicion = ObtenerPosicionCuadrante(jugador.position);

        if (nuevaPosicion != posicionAnteriorJugador)
        {
            VerificarCambioCuadrante(nuevaPosicion);
            posicionAnteriorJugador = nuevaPosicion;
        }
    }

    void InicializarMatrizCuadrantes()
    {
        cuadrantesReplantados = new bool[filas * subdivisionesEnFilas, columnas * subdivisionesEnColumnas];
        
    }

    void DividirTerrenoEnCuadrantes()
    {
        float tamanoX = terreno.terrainData.size.x;
        float tamanoZ = terreno.terrainData.size.z;

        float tamanoCuadranteX = tamanoX / (columnas * subdivisionesEnColumnas);
        float tamanoCuadranteZ = tamanoZ / (filas * subdivisionesEnFilas);

        for (int fila = 0; fila < filas * subdivisionesEnFilas; fila++)
        {
            for (int columna = 0; columna < columnas * subdivisionesEnColumnas; columna++)
            {
                cuadrantesReplantados[fila, columna] = false; // Inicializa todos los cuadrantes como no replantados
            }
        }
    }

    void VerificarCambioCuadrante(Vector3 nuevaPosicion)
    {
        int filaActual = Mathf.FloorToInt(nuevaPosicion.z);
        int columnaActual = Mathf.FloorToInt(nuevaPosicion.x);

        Debug.Log("El jugador ha cambiado de cuadrante. Fila: " + filaActual + ", Columna: " + columnaActual);
    }

   public Vector3 ObtenerPosicionCuadrante(Vector3 posicion)
    {
        float x = Mathf.Floor(posicion.x / terreno.terrainData.size.x * columnas * subdivisionesEnColumnas);
        float z = Mathf.Floor(posicion.z / terreno.terrainData.size.z * filas * subdivisionesEnFilas);

        return new Vector3(x, 0f, z);
    }

    public void ActualizarCuadrante(Vector3 posicion)
    {
        // Obtener el tamaño total del terreno
        float tamanoX = terreno.terrainData.size.x;
        float tamanoZ = terreno.terrainData.size.z;

        // Calcular el tamaño de cada cuadrante
        float tamanoCuadranteX = tamanoX / columnas;
        float tamanoCuadranteZ = tamanoZ / filas;

        // Convierte las coordenadas del mundo a las coordenadas locales del objeto Map_Matrix
        Vector3 posicionLocal = transform.InverseTransformPoint(posicion);

        // Calcula la posición en términos de la matriz de cuadrantes
        int fila = Mathf.FloorToInt(posicionLocal.z / tamanoCuadranteZ);
        int columna = Mathf.FloorToInt(posicionLocal.x / tamanoCuadranteX);

    }
}