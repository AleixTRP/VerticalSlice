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
   

        // Espera hasta que la animaci�n termine
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        // Realiza la plantaci�n solo si a�n hay �rboles en el inventario
        Inventory playerInventory = GetComponent<Inventory>();
        if (playerInventory != null && playerInventory.InventoryCount > 0)
        {
            PlantTreeInFront();
        }

        // Restablece la animaci�n
        animator.SetBool("plant", false);
        tool.SetActive(true);
        
    }

    void PlantTreeInFront()
    {
        Inventory playerInventory = GetComponent<Inventory>();
        CutPineTree TreeMap = GetComponent<CutPineTree>();  


        if (playerInventory != null && playerInventory.InventoryCount > 0)
        {
            // Obt�n el �rbol del inventario
            GameObject treeObject = playerInventory.GetFirstTreeFromInventory();

      

            if (treeObject != null)
            {
                // Obt�n la posici�n actual del jugador
                Vector3 playerPosition = transform.position;

                // Calcula la posici�n para plantar en la direcci�n (forward) del jugador
                Vector3 plantPosition = playerPosition + transform.forward * distanceFromPlayer;
               
                                         
                // Establece la posici�n del �rbol en la posici�n calculada
                treeObject.transform.position = plantPosition;

                Instantiate(particleSystem, plantPosition, Quaternion.identity);
                
                
                          
                
                // Actualiza el cuadrante despu�s de plantar el �rbol
                mapMatrix.ActualizarCuadrante(plantPosition);

                // Inicia la corutina de crecimiento para este �rbol espec�fico
                StartCoroutine(treeObject.GetComponent<GrowPlant>().GrowTree());

                // Elimina el �rbol del inventario
                playerInventory.RemoveFromInventory(treeObject);

             
                Debug.Log("�rbol plantado delante del jugador en la posici�n: " + plantPosition);
            }
            else
            {
                Debug.LogWarning("No se pudo obtener el �rbol del inventario.");
            }
        }
        else
        {
            Debug.LogWarning("Inventario no v�lido o sin �rboles.");
        }
    }
}