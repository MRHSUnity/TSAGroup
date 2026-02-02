using UnityEngine;

public class HealthUIPlayer : MonoBehaviour
{
    public float Health, MaxHealth, pastHealth;

    [SerializeField] private HealthBarUI healthBar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthBar.setMaxHealth(MaxHealth);
        pastHealth = Health;
    }

    // Update is called once per frame
    void Update()
    {
        if (pastHealth>Health)
        {
            
            setHealh(Health-pastHealth);
            pastHealth=Health;
        }
        
    }
    public void setHealh(float healthChange)
    {
        Health += healthChange;
        Health = Mathf.Clamp(Health, 0, MaxHealth);

        healthBar.setHealth(Health);
    }
}
