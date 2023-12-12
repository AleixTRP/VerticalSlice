using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay_Camera : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private float targetDistance = 5f;
    [SerializeField] private float cameraLerp = 12f;
    [SerializeField] private float rotationLerp = 10f; // Nuevo parámetro para el suavizado de rotación
    [SerializeField] private float heightOffset = 2.0f;
    [SerializeField] private float maxVerticalAngle = 50f;

    private Vector2 cameraInput;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Audio_Manager.instance.Play("MenuSound");
    }

    private void LateUpdate()
    {
        if (!MenuPause.GamePaused)
        {
            HandleCameraInput();
            HandleCameraRotation();
            HandleCameraPosition();
        }
    }

    private void HandleCameraInput()
    {
        cameraInput += Input_Manager._INPUT_MANAGER.GetCameraValue();
        cameraInput.y = Mathf.Clamp(cameraInput.y, -maxVerticalAngle, maxVerticalAngle);
    }

    private void HandleCameraRotation()
    {
        Quaternion targetRotation = Quaternion.Euler(cameraInput.y, cameraInput.x, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationLerp * Time.deltaTime);
    }

    private void HandleCameraPosition()
    {
        Vector3 targetPosition = target.transform.position + Vector3.up * heightOffset;
        Vector3 desiredPosition = targetPosition - transform.forward * targetDistance;

        transform.position = Vector3.Lerp(transform.position, desiredPosition, cameraLerp * Time.deltaTime);
    }
}