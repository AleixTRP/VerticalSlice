using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    private Controller cont;

    private Vector3 movementInput = Vector3.zero;
    
    private Vector3 finalVelocity = Vector3.zero;

    [SerializeField]
    private GameObject cam;

    [SerializeField]
    private float velocityXZ = 5f;

    [SerializeField]
    private float acceleration = 5f;


    private void Awake()
    {
        cont = GetComponent<Controller>();

    }

    private void Update()
    {
        Vector2 inputVector = InputManager._INPUT_MANAGER.GetLeftAxisValue();
        movementInput = new Vector3(inputVector.x, 0f, inputVector.y);
        movementInput.Normalize();
        
        finalVelocity.x = Mathf.MoveTowards(finalVelocity.x, velocityXZ, acceleration * Time.deltaTime);
        finalVelocity.z = Mathf.MoveTowards(finalVelocity.z, velocityXZ, acceleration * Time.deltaTime);


    }
}







