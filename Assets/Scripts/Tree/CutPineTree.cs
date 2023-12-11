using System.Collections;
using UnityEngine;

public class CutPineTree : MonoBehaviour
{
    private bool canHit;
    private bool hasTouchedThisFrame;
    private bool isCuttingAnimationPlaying;
    private bool cutButtonPressed;

    [SerializeField] private TreeScriptableObject Stree;
    private float treelife;
    [SerializeField] private GameObject gm;
    [SerializeField] private GameObject smallPinePrefab;
    private Inventory playerInventory;
    [SerializeField] private Animator animator;

    private void Start()
    {
        treelife = Stree.hitTree;
        gm = transform.parent.gameObject;
        isCuttingAnimationPlaying = false;
        cutButtonPressed = false;
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
            if (Input_Manager._INPUT_MANAGER.GetButtonCut())
            {
                cutButtonPressed = true;
            }

            if (cutButtonPressed && treelife > 0 && !isCuttingAnimationPlaying)
            {
                Debug.Log("Entra en la animación de Talar");

                // Check if the animation is not already playing
                if (!isCuttingAnimationPlaying)
                {
                    StartCoroutine(PlayCuttingAnimation());
                }

                treelife--;
                hasTouchedThisFrame = true;
            }
        }
    }

    private IEnumerator PlayCuttingAnimation()
    {
        isCuttingAnimationPlaying = true;
        animator.SetBool("cut", true);

        // Wait until the animation is complete
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        animator.SetBool("cut", false);
        isCuttingAnimationPlaying = false;

        // Check if there is still life left in the tree
        if (treelife > 0)
        {
            // Reset the cutButtonPressed flag to allow triggering the animation again
            cutButtonPressed = false;
        }
        else
        {
            // Perform destruction logic after the last animation
            if (Random.value < Stree.luckyTree && smallPinePrefab != null)
            {
                Debug.Log("ArbolSpawn");
                GameObject smallTreeInstance = Instantiate(smallPinePrefab, gm.transform.position, gm.transform.rotation);
            }
            Audio_Manager.instance.Play("TreeFall");

            Destroy(gm);
        }
    }
}