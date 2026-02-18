using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager instance; // Singleton instance
    //public Text currencyText; // Reference to UI Text element
    public TextMeshProUGUI currencyText;

    private int currentBalance;
    private const string BalanceKey = "PlayerBalance"; // Key for PlayerPrefs

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes if needed
        }
        else
        {
            Destroy(gameObject);
        }

        LoadBalance();
        UpdateUI();
    }

    public void AddCurrency(int amount)
    {
        currentBalance += amount;
        SaveBalance();
        UpdateUI();
    }

    public bool SpendCurrency(int amount)
    {
        if (currentBalance >= amount)
        {
            currentBalance -= amount;
            SaveBalance();
            UpdateUI();
            return true; // Purchase successful
        }
        return false; // Not enough money
    }

    public int GetBalance()
    {
        return currentBalance;
    }

    private void LoadBalance()
    {
        // Load balance from PlayerPrefs, default to 0 if not found
        currentBalance = PlayerPrefs.GetInt(BalanceKey, 0);
    }

    private void SaveBalance()
    {
        PlayerPrefs.SetInt(BalanceKey, currentBalance);
        PlayerPrefs.Save(); // Ensure it's saved immediately
    }

    private void UpdateUI()
    {
        if (currencyText != null)
        {
            currencyText.text = currentBalance.ToString("#,0");
        }
    }
}
