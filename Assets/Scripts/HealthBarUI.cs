using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public float health, maxHealth, width, height;

    [SerializeField] private RectTransform healthBar;

    public void setMaxHealth(float maxHealtha)
    {
        maxHealth = maxHealtha;
    }

    public void setHealth(float healtha)
    {
        health = healtha;
        float newWidth = (health/maxHealth) * width;

        healthBar.sizeDelta = new Vector2(newWidth, height);
    }
    
}
