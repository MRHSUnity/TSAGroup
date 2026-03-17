using UnityEngine;

public class ShopSensor : MonoBehaviour
{
    public LayerMask player;
    public GameObject ShopPanel;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & player) != 0)
        {
            ShopPanel.SetActive(true);

        }
    }
}
