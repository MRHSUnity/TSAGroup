using System.Collections;
using UnityEngine;

public class SkeleAttack : MonoBehaviour
{
    public int damage = 3;
    public LayerMask player;
    public float knockbackForce = 8f;
    public float knockbackUpwards = 2f;
    public float stunDuration = 0.5f;
    public Rigidbody2D rb;
    public MonoBehaviour movementScript;
    public Animator anim;
   private void OnTriggerEnter2D(Collider2D collision)
   { 
       if (((1 << collision.gameObject.layer) & player) != 0)
       {
           HealthUIPlayer health = collision.GetComponent<HealthUIPlayer>();
           if (health != null)
           { 
               health.healthChange(damage);
               Debug.Log("Player hit for " + damage + " damage.");
           }

            rb = collision.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // Determine direction: if player is left of attacker, push left (-1), otherwise right (+1)
                float direction = collision.transform.position.x < transform.position.x ? -1f : 1f;
           
                // Apply knockback velocity
                rb.linearVelocity = new Vector2(knockbackForce * direction, knockbackUpwards);
           
                // Try to disable common player movement scripts during stun (if present)
                var playerController = collision.GetComponent("PlayerController") as MonoBehaviour;

                    movementScript = playerController;

                    movementScript.enabled = false;
       
                anim.SetBool("isStunned", true);
                
            }
   }
}

    
}