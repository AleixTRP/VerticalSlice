using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay_Camera : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private float targetDistance;
    [SerializeField] private float cameraLerp; //12f


    private Vector2 GetCamera;

    private RaycastHit hit;

    private void LateUpdate()
    {
        if (!MenuPause.GamePaused) // Verifica si el juego no está pausado
        {
            GetCamera += Input_Manager._INPUT_MANAGER.GetCameraValue();

            GetCamera.y = Mathf.Clamp(GetCamera.y, -50f, 50f);

            transform.eulerAngles = new Vector3(GetCamera.y, GetCamera.x, 0);

            transform.position = Vector3.Lerp(transform.position, target.transform.position - transform.forward * targetDistance, cameraLerp * Time.deltaTime);

            if (Physics.Linecast(target.transform.position, transform.position, out hit))
            {
                transform.position = hit.point;
            }
        }
    }
}
