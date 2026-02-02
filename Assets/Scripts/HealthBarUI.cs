using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public float Health, MaxHealth, width, height;

    [SerializeField] private RectTransform healthBar;

    public void setMaxHealth(float maxHealth)
    {
        MaxHealth = maxHealth;
    }

    public void setHealth(float health)
    {
        Health = health;
        float newWidth = (Health/MaxHealth) * width;

        healthBar.sizeDelta = new Vector2(newWidth, height);
    }
    
}
