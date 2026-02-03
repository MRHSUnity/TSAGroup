using UnityEngine;

public class SkeleAttack : MonoBehaviour
{
    public int damage = 3;
    public LayerMask player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
   private void OnTriggerEnter2D(Collider2D collision)
   { 
       if (((1 << collision.gameObject.layer) & player) != 0)
       {
           HealthUIPlayer health = collision.GetComponent<HealthUIPlayer>();
           if (health != null)
           { 
               health.health -= damage; 
               Debug.Log("Player hit for " + damage + " damage.");
           }
       }
   }
}
