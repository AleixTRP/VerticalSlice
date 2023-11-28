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

    //[SerializeField]
    // private GameObject clouds;

    //[SerializeField]
    //private GameObject stars;

    private float sunX;

    private const float hoursInADay = 24f;

    private void Update()
    {
        currentHour += Time.deltaTime * (hoursInADay / (60 * timeInMinutes));
        if (currentHour >= hoursInADay)
        {
            currentHour = 0;
        }
        RotateSun();
        UpdateEnvironment();
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

    private void UpdateEnvironment()
    {
        if (currentHour >= 6 && currentHour <= 18)
        {
          
            // clouds.SetActive(true);
            //stars.SetActive(false);
        }
        else
        {
           
            // clouds.SetActive(false);
            // stars.SetActive(true);
        }
    }
}

