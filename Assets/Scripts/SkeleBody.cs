using System.Collections;
using UnityEngine;

public class SkeleBody : MonoBehaviour
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
        if ((((1 << collision.gameObject.layer) & player) != 0)&& !anim.GetBool("isAttacking"))
        {
            rb = collision.GetComponent<Rigidbody2D>();
            if (rb != null)
            {   
                // Determine direction: if player is left of attacker, push left (-1), otherwise right (+1)
                float direction = collision.transform.position.x < transform.position.x ? -1f : 1f;
                // Apply knockback velocity
                rb.constraints = RigidbodyConstraints2D.FreezeRotation; // Unfreeze player to allow knockback
                rb.linearVelocity = new Vector2(knockbackForce * direction, knockbackUpwards);
           
                // Try to disable common player movement scripts during stun (if present)
                var playerController = collision.GetComponent("PlayerController") as MonoBehaviour;

      
                anim.SetBool("isStunned", true);
                
            }
        }
    }

    
}
