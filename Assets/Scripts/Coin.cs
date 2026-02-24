using UnityEngine;

public class Coin : MonoBehaviour
{
    public LayerMask player;
    public GameObject coin;
    public CurrencyManager currencyManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & player) != 0)
        {
            currencyManager.AddCurrency(1);
            Destroy(coin);

        }
    }
}
