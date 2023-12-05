using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class CutPineTree : MonoBehaviour
{
    private bool canHit;
    private bool hasTouchedThisFrame;
    private bool isCuttingAnimationPlaying;

    [SerializeField] private TreeScriptableObject Stree;

    private float treelife;

    [SerializeField]
    private GameObject gm;

    [SerializeField]
    private GameObject smallPinePrefab;

    private Inventory playerInventory;

    [SerializeField]
    private Animator animator;




  

    private void Start()
    {
       
        treelife = Stree.hitTree;
        gm = transform.parent.gameObject;
    
        isCuttingAnimationPlaying = false;

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
            // El jugador pasó por encima del árbol pequeño, agregar al inventario y destruirlo
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
        hasTouchedThisFrame = false;

        if (canHit && !hasTouchedThisFrame)
        {
          
            if (Input_Manager._INPUT_MANAGER.GetButtonCut() && treelife > 0 && !isCuttingAnimationPlaying)
            {
                Debug.Log("Entra en la animación de Talar");

                // Inicia la animación de corte
                StartCoroutine(PlayCuttingAnimation());
              

                // Reduce la vida del árbol
                treelife--;
                hasTouchedThisFrame = true;
               
                if (treelife <= 0)
                {
                    
                    Debug.Log("Destruir");
                    animator.SetBool("cut", false);
                                      
                    if (Random.value < Stree.luckyTree && smallPinePrefab != null)
                    {
                        Debug.Log("ArbolSpawn");
                        // Instanciar el árbol pequeño en la escena
                        GameObject smallTreeInstance = Instantiate(smallPinePrefab, gm.transform.position, gm.transform.rotation);
                    }

                    // Destruir el objeto padre (árbol completo)
                   Destroy(gm);
                }
            }
        }
    }

  private IEnumerator PlayCuttingAnimation()
    {
        // Marca la animación como en reproducción
        isCuttingAnimationPlaying = true;

        // Inicia la animación
        animator.SetBool("cut", true);
      
      
       
        // Espera hasta que la animación esté completa
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        // Marca la animación como finalizada
        animator.SetBool("cut", false);
        isCuttingAnimationPlaying = false;
    }

    public GameObject GetPreviewTree()
    {
        return Stree.gameObject;
    }


}
