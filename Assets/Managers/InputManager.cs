using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using static UnityEditor.Timeline.TimelinePlaybackControls;


public class InputManager : MonoBehaviour
{
    private PlayerInputs playerInputs;
    public static InputManager _INPUT_MANAGER;

    private Vector2 leftAxisValue = Vector2.zero;


    private void Awake()
    {
        if (_INPUT_MANAGER != null && _INPUT_MANAGER != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            playerInputs = new PlayerInputs();
            playerInputs.Character.Enable();
            playerInputs.Character.Move.performed += LeftAxisUpdate;


            _INPUT_MANAGER = this;
            DontDestroyOnLoad(this);
        }

    }
    private void Update()
    {
   
        InputSystem.Update();

    }

    private void LeftAxisUpdate(InputAction.CallbackContext context)
    {

        leftAxisValue = context.ReadValue<Vector2>();

    }

    public Vector2 GetLeftAxisValue()
    {
        return this.leftAxisValue;
    }

}