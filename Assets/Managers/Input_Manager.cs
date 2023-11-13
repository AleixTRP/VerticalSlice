using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class Input_Manager : MonoBehaviour
{
    private PlayerInputActions playerInputs;
    public static Input_Manager _INPUT_MANAGER;

    private Vector2 leftAxisValue = Vector2.zero;

    private Vector2 cam = Vector2.zero;

    private float buttonPlant = 0f;

    


    private void Awake()
    {
        if (_INPUT_MANAGER != null && _INPUT_MANAGER != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            playerInputs = new PlayerInputActions();
            playerInputs.Character.Enable();
            playerInputs.Character.Move.performed += LeftAxisUpdate;
            playerInputs.Character.Camera.performed += CameraMovement;
            playerInputs.Character.Grow.performed += GrowButtonPlant;
        
            _INPUT_MANAGER = this;
            DontDestroyOnLoad(this);
        }

    }
    private void Update()
    {

        buttonPlant += Time.deltaTime;

        InputSystem.Update();

    }
  

    //Vector2
    private void LeftAxisUpdate(InputAction.CallbackContext context)
    {

        leftAxisValue = context.ReadValue<Vector2>();

    }

    //Vector2
    public Vector2 GetLeftAxisValue()
    {
        return this.leftAxisValue;
    }

    //Button
    private void GrowButtonPlant(InputAction.CallbackContext context)
    {

        buttonPlant = 0f;
    }

    //Button
    public bool GetButtonPlant()
    {
        return this.buttonPlant == 0f;
    }

    //Vector2
    private void CameraMovement(InputAction.CallbackContext context)
    {

        cam = context.ReadValue<Vector2>();

    }

    //Vector2
    public Vector2 GetCameraValue()
    {
        return this.cam;
    }

  
}
