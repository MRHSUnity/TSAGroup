using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DracHealth : MonoBehaviour
{
    public int health, MaxHealth;
    [SerializeField] private HealthBarUI healthBar;

    public TextMeshProUGUI healthText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthBar.setMaxHealth(MaxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = health + " / " + MaxHealth;

        if (health <= 0)
        {
            SceneManager.LoadScene("WinScene");
        }
        healthBar.setHealth(health);

    }
    public void healthChange(int healthChange)
    {
        health -= healthChange;
        health = Mathf.Clamp(health, 0, MaxHealth);

    }

}
