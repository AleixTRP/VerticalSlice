using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_MotherTree : MonoBehaviour
{
    public TMP_Text currentLifeText;
    public TMP_Text maxLifeText;
    public Slider lifeSlider;

    private MotherTree motherTreeComponent;

    void Start()
    {
        motherTreeComponent = FindObjectOfType<MotherTree>();
        UpdateLifeUI(motherTreeComponent.GetCurrentLife(), motherTreeComponent.GetMaxLife());
    }


    public void UpdateLifeUI(float currentLife, float maxLife)
    {
        if (currentLifeText != null && maxLifeText != null && lifeSlider != null)
        {
            currentLifeText.text =  currentLife.ToString();
            maxLifeText.text =  maxLife.ToString();
            lifeSlider.value = currentLife / maxLife;
        }
    }
}