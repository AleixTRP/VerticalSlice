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

        // Calcular velocidad XZ
        finalVelocity.x = direction.x * velocityXZ;
        finalVelocity.z = direction.z * velocityXZ;

        float speed = finalVelocity.z;

       

        // Asignar dirección Y
        direction.y = -1f;
        controller.Move(finalVelocity * Time.deltaTime);
    }
}



