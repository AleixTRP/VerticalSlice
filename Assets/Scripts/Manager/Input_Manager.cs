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

    private float buttonCut = 0f;
   
    private float rightClick = 0f;
    
    private float leftClick = 0f;
   
    private float escButton = 0f;

    


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
            playerInputs.Character.Cut.performed += CutButtonAction;
            playerInputs.Character.RightClick.performed += RightClickButton;
            playerInputs.Character.LeftClick.performed += LeftClickButton;
            playerInputs.Character.Esc.performed += EscButton;
        
            _INPUT_MANAGER = this;
            DontDestroyOnLoad(this);
        }

    }
    private void Update()
    {

        buttonPlant += Time.deltaTime;

        buttonCut += Time.deltaTime;

        leftClick += Time.deltaTime;
       
        rightClick += Time.deltaTime;
        
        escButton += Time.deltaTime;


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
    //Button
    private void CutButtonAction(InputAction.CallbackContext context)
    {

        buttonCut = 0f;
    }

    //Button
    public bool GetButtonCut()
    {
        return this.buttonCut == 0f;
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
    private void RightClickButton(InputAction.CallbackContext context)
    {

        rightClick = 0f;
    }

    //Button
    public bool GetRightClick()
    {
        return this.rightClick == 0f;
    }

    private void LeftClickButton(InputAction.CallbackContext context)
    {

        leftClick = 0f;
    }

    //Button
    public bool GetLeftClick()
    {
        return this.leftClick == 0f;
    }

    private void EscButton(InputAction.CallbackContext context)
    {

        escButton = 0f;
    }

    //Button
    public bool GetEscButton()
    {
        return this.escButton == 0f;
    }



}
