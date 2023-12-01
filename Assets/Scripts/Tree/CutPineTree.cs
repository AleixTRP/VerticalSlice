using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class CutPineTree : MonoBehaviour
{
    private bool canHit;

    [SerializeField]
    private int life = 3;

    [SerializeField]
    private GameObject gm;

    [SerializeField]
    private GameObject smallPinePrefab;

    private Inventory playerInventory;

    [SerializeField]
    List<GameObject> smallPlants;

    [SerializeField]
    private Animator animator;




    private void Start()
    {
        gm = transform.parent.gameObject;

     
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("RangoCharacter"))
        {
            Debug.Log("Entra");
            canHit = true;
        }
        else if (other.gameObject.CompareTag("SmallPine") && canHit)
        {
            // El jugador pas� por encima del �rbol peque�o, agregar al inventario y destruirlo
            playerInventory = other.GetComponent<Inventory>();
            if (playerInventory != null)
            {
                
                playerInventory.AddToInventory(other.gameObject);
            }

            Destroy(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("RangoCharacter"))
        {
            Debug.Log("Sale");
            canHit = false;
        }
    }

    private void Update()
    {
        if (canHit)
        {
            if (Input_Manager._INPUT_MANAGER.GetButtonCut())
            {
                Debug.Log("Entra en la animaci�n de Talar");
                animator.SetBool("cut", true);
                life--;
            }

            if (life <= 0)
            {
                
                animator.SetBool("cut", false);
                Debug.Log("Destruir");

                // Generar un �rbol peque�o con un 70% de probabilidad
                if (Random.value < 0.7f && smallPinePrefab != null)
                {
                    // Instanciar el �rbol peque�o en la escena
                    GameObject smallTreeInstance = Instantiate(smallPinePrefab, gm.transform.position, gm.transform.rotation);

                    
                }


                // Destruir el objeto padre (�rbol completo)
                Destroy(gm);
            }
        }
    }
}