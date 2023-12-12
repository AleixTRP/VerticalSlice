using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantTree : MonoBehaviour
{
    private Map_Matrix mapMatrix;

    [SerializeField] private float distanceFromPlayer = 2f;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private GameObject tool;

    [SerializeField]
    private ParticleSystem particleSystem;


    void Start()
    {
          
        mapMatrix = FindObjectOfType<Map_Matrix>();
    }

    void Update()
    {
        if (Input_Manager._INPUT_MANAGER.GetButtonPlant() && !animator.GetBool("plant"))
        {
            
            StartCoroutine(PlantTreeWithAnimation());
        }
    }

    IEnumerator PlantTreeWithAnimation()
    {
         
        tool.SetActive(false); 
        animator.SetBool("plant", true);
        Audio_Manager.instance.Play("SpellPlant");
   

        // Espera hasta que la animación termine
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        // Realiza la plantación solo si aún hay árboles en el inventario
        Inventory playerInventory = GetComponent<Inventory>();
        if (playerInventory != null && playerInventory.InventoryCount > 0)
        {
            PlantTreeInFront();
        }

        // Restablece la animación
        animator.SetBool("plant", false);
        tool.SetActive(true);
        
    }

    void PlantTreeInFront()
    {
        Inventory playerInventory = GetComponent<Inventory>();
        CutPineTree TreeMap = GetComponent<CutPineTree>();  


        if (playerInventory != null && playerInventory.InventoryCount > 0)
        {
            // Obtén el árbol del inventario
            GameObject treeObject = playerInventory.GetFirstTreeFromInventory();

      

            if (treeObject != null)
            {
                // Obtén la posición actual del jugador
                Vector3 playerPosition = transform.position;

                // Calcula la posición para plantar en la dirección (forward) del jugador
                Vector3 plantPosition = playerPosition + transform.forward * distanceFromPlayer;
               
                                         
                // Establece la posición del árbol en la posición calculada
                treeObject.transform.position = plantPosition;

                Instantiate(particleSystem, plantPosition, Quaternion.identity);
                
                
                          
                
                // Actualiza el cuadrante después de plantar el árbol
                mapMatrix.ActualizarCuadrante(plantPosition);

                // Inicia la corutina de crecimiento para este árbol específico
                StartCoroutine(treeObject.GetComponent<GrowPlant>().GrowTree());

                // Elimina el árbol del inventario
                playerInventory.RemoveFromInventory(treeObject);

             
                Debug.Log("Árbol plantado delante del jugador en la posición: " + plantPosition);
            }
            else
            {
                Debug.LogWarning("No se pudo obtener el árbol del inventario.");
            }
        }
        else
        {
            Debug.LogWarning("Inventario no válido o sin árboles.");
        }
    }
}