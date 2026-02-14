using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthUIPlayer : MonoBehaviour
{
    public float health, MaxHealth;

    [SerializeField] private HealthBarUI healthBar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthBar.setMaxHealth(MaxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            SceneManager.LoadScene("DeathScene");

        }
        
    }
    public void healthChange(float healthChange)
    {
        health -= healthChange;
        health = Mathf.Clamp(health, 0, MaxHealth);

        healthBar.setHealth(health);
    }
}
