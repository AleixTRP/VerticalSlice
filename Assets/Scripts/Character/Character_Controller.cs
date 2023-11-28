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

    private float targetAngle;


    [SerializeField] private float Rotation_Smoothness;
    private Quaternion Quaternion_Rotate_From;
    private Quaternion Quaternion_Rotate_To;

    Vector3 desiredMoveDirection;




    private void Awake()
    {
        controller = GetComponent<CharacterController>();

    }

    private void Update()
    {
        Vector3 direction = Quaternion.Euler(0f, cam.transform.eulerAngles.y, 0f) * new Vector3(movementInput.x, 0f, movementInput.z);
        direction.Normalize();

        Vector2 inputVector = Input_Manager._INPUT_MANAGER.GetLeftAxisValue();
        movementInput = new Vector3(inputVector.x, 0f, inputVector.y);
        movementInput.Normalize();



        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }

        // Calcular velocidad XZ
        finalVelocity.x = direction.x * velocityXZ;
        finalVelocity.z = direction.z * velocityXZ;

        float speed = Mathf.Abs(finalVelocity.magnitude);

     


        // Asignar dirección Y
        direction.y = -1f;
        controller.Move(finalVelocity * Time.deltaTime);

        Debug.Log(finalVelocity.z);

        animator.SetFloat("velocity", speed);

        
    }

   

}




