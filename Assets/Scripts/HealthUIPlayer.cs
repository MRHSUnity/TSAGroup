using UnityEngine;

public class HealthUIPlayer : MonoBehaviour
{
    public float health, MaxHealth, pastHealth;

    [SerializeField] private HealthBarUI healthBar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthBar.setMaxHealth(MaxHealth);
        pastHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        if (pastHealth>health)
        {
            
            setHealh(health-pastHealth);
            pastHealth=health;
        }
        
    }
    public void setHealh(float healthChange)
    {
        health += healthChange;
        health = Mathf.Clamp(health, 0, MaxHealth);

        healthBar.setHealth(health);
    }
}
