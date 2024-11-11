using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI healthText;
    public Slider expSlider;
    public Slider healthSlider;

    public void UpdateAmmo(int newAmmo, int maxAmmo)
    {
        ammoText.text = $"{newAmmo}/{maxAmmo}";
    }

    public void UpdateHealth(float newHealth, int maxHealth)
    {
        healthText.text = $"{Convert.ToInt32(newHealth)}/{maxHealth}";
        healthSlider.value = newHealth;
        healthSlider.maxValue = maxHealth;
    }

    public void UpdateExp(float newExp, float maxExp)
    {
        expSlider.maxValue = maxExp;
        expSlider.value = newExp;
    }
}
