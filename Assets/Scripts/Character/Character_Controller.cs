using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class Character_Controller : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 finalVelocity = Vector3.zero;

    [SerializeField]
    private float velocityXZ = 5f;

    private Vector3 movementInput = Vector3.zero;

    [SerializeField]
    private GameObject cam;

    [SerializeField]
    private Animator animator;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // Obtener la entrada de movimiento
        Vector2 inputVector = Input_Manager._INPUT_MANAGER.GetLeftAxisValue();
        movementInput = new Vector3(inputVector.x, 0f, inputVector.y);
        movementInput.Normalize();

        // Calcular la direcci�n basada en la rotaci�n de la c�mara
        Vector3 direction = Quaternion.Euler(0f, cam.transform.eulerAngles.y, 0f) * movementInput;
        direction.Normalize();

        if (direction != Vector3.zero)
        {
            // Calcular la rotaci�n deseada hacia la direcci�n de la velocidad
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }

        // Calcular la velocidad en el plano XZ
        finalVelocity.x = direction.x * velocityXZ;
        finalVelocity.z = direction.z * velocityXZ;

        // Mover al personaje
        controller.Move(finalVelocity * Time.deltaTime);

        // Debug.Log(finalVelocity.z);

        // Establecer el par�metro del animador para la velocidad
        float speed = Mathf.Abs(finalVelocity.magnitude);
        animator.SetFloat("velocity", speed);

        animator.SetBool("cut", false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Maceta"))
        {
            Input_Manager._INPUT_MANAGER.GetButtonCut();
            animator.SetBool("cut", true);
            animator.SetBool("RangeCut", true);

        }
        
    }
    
}



