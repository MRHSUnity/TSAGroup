using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class HealthUIPlayer : MonoBehaviour
{
    private static int savedMaxHealth = 10;
    public int health, MaxHealth;
    public GameObject CurrencyManager;
    [SerializeField] private HealthBarUI healthBar;
    public TextMeshProUGUI healthText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MaxHealth=savedMaxHealth;
        health = MaxHealth;
        healthBar.setMaxHealth(MaxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = health + " / " + MaxHealth;

        if (health <= 0)
        {
            SceneManager.LoadScene("DeathScene");
            
        }
        
    }
    public void addMaxHealth(int health)
    {
        MaxHealth += health;
        savedMaxHealth = MaxHealth;
        this.health += health;
    }
    public void shopHealth(int h)
    {
        if (CurrencyManager.GetComponent<CurrencyManager>().canSpend)
        {
            addMaxHealth(h);
        }
    }
    public void healthChange(int healthChange)
    {
        health -= healthChange;
        health = Mathf.Clamp(health, 0, MaxHealth);

        healthBar.setHealth(health);
    }
}
