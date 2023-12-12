using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_MotherTree : MonoBehaviour
{

   [SerializeField] private  Slider lifeSlider;
    [SerializeField] private TMP_Text hourText; // Añade un objeto Text para mostrar la hora
    [SerializeField] private DayNight dayNightCycle; 

    private MotherTree motherTreeComponent;

    void Start()
    {
        motherTreeComponent = FindObjectOfType<MotherTree>();
        UpdateLifeUI(motherTreeComponent.GetCurrentLife(), motherTreeComponent.GetMaxLife());
    }

    void Update()
    {
      
        if (dayNightCycle != null && hourText != null)
        {
            float currentHour = dayNightCycle.GetCurrentHour();
            hourText.text =  Mathf.Floor(currentHour).ToString("00") + ":" + Mathf.Floor((currentHour % 1) * 60).ToString("00");
        }
    }

    public void UpdateLifeUI(float currentLife, float maxLife)
    {
        if (lifeSlider != null)
        {
            lifeSlider.value = currentLife / maxLife;
        }
    }
}