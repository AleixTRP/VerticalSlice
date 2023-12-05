using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNight : MonoBehaviour
{
    [SerializeField]
    [Range(0.0f, 24f)] private float currentHour = 12f;

    [SerializeField]
    private Transform Sun;

    [SerializeField]
    private float timeInMinutes = 1f;

    private float sunX;

    private const float hoursInADay = 24f;



    public float GetCurrentHour()
    {
        return currentHour;
    }
    private void Update()
    {
        currentHour += Time.deltaTime * (hoursInADay / (60 * timeInMinutes));
        if (currentHour >= hoursInADay)
        {
            currentHour = 0;
        }
        RotateSun();
       
    }

    private void RotateSun()
    {
        sunX = 15 * currentHour;

        Sun.localEulerAngles = new Vector3(sunX, 0, 0);

        if (currentHour < 6 || currentHour > 18)
        {
            Sun.GetComponent<Light>().intensity = 0;
        }
        else
        {
            Sun.GetComponent<Light>().intensity = 1.5f;
        }
    }
}

