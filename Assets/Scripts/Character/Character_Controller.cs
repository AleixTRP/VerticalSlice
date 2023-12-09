using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Character_Controller : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 finalVelocity = Vector3.zero;
    private float verticalVelocity;

    [SerializeField] private float velocityXZ = 5f;
    [SerializeField] private float gravity = 9.8f;
    [SerializeField] private GameObject cam;
    [SerializeField] private Animator animator;
   

    private void Awake()
    {
        
        controller = GetComponent<CharacterController>();
        controller.enableOverlapRecovery = true; // Habilitar recuperación de solapamiento
    }

 


    private void Update()
    {
        // Obtener la entrada de movimiento
        Vector2 inputVector = Input_Manager._INPUT_MANAGER.GetLeftAxisValue();
        Vector3 movementInput = new Vector3(inputVector.x, 0f, inputVector.y);
        movementInput.Normalize();

        // Calcular la dirección basada en la rotación de la cámara
        Vector3 direction = Quaternion.Euler(0f, cam.transform.eulerAngles.y, 0f) * movementInput;
        direction.Normalize();

        if (direction != Vector3.zero)
        {
            // Calcular la rotación deseada hacia la dirección de la velocidad
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }

        // Calcular la velocidad en el plano XZ
        finalVelocity.x = direction.x * velocityXZ;
        finalVelocity.z = direction.z * velocityXZ;

        // Aplicar gravedad
        if (controller.isGrounded)
        {
            verticalVelocity = -0.5f;
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        // Incluir la componente vertical a la velocidad final
        finalVelocity.y = verticalVelocity;

        // Mover al personaje
        controller.Move(finalVelocity * Time.deltaTime);

        // Establecer el parámetro del animador para la velocidad
        float speed = Mathf.Abs(finalVelocity.magnitude);

        Debug.Log(speed);
        animator.SetFloat("velocity", speed);
    }
}
