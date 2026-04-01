using UnityEngine;

public class OrbitProjectile2D : MonoBehaviour
{
    [Header("Homing Settings")]
    public float speed = 5f;
    public float homingDuration = 0.2f; // how long it tracks the player
    public float lifetime = 5f;         // how long it exists before disappearing

    private Transform target;
    private float homingTimer = 0f;
    private float lifeTimer = 0f;
    private bool launched = false;
    
    public int damage = 3;
    private Rigidbody2D rb;
    private Animator anim;
    public float knockbackForce = 1f;
    public float knockbackUpwards = 1f;
    public float stunDuration = 0.2f;

    public void Launch(Transform player)
    {
        target = player;
        launched = true;
        homingTimer = 0f;
        lifeTimer = 0f;
        
        transform.parent = null;

    }

    void Update()
    {
        if (!launched) return;

        lifeTimer += Time.deltaTime;

        // Destroy after lifetime expires
        if (lifeTimer >= lifetime)
        {
            Destroy(gameObject);
            return;
        }

        homingTimer += Time.deltaTime;

        // Only track player for homingDuration
        if (homingTimer <= homingDuration && target != null)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            transform.position += (Vector3)(direction * speed * Time.deltaTime);
        }
        else
        {
            // Continue in last direction
            transform.position += transform.up * speed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            HealthUIPlayer health = collision.GetComponent<HealthUIPlayer>();
            if (health != null)
            {
                health.healthChange(damage);
                Debug.Log("Player hit for " + damage + " damage.");
            }
            //
            //
            // rb = collision.GetComponent<Rigidbody2D>();
            // if (rb != null)
            // {  
            //     // Determine direction: if player is left of attacker, push left (-1), otherwise right (+1)
            //     float direction = collision.transform.position.x < transform.position.x ? -1f : 1f;
            //     // Apply knockback velocity
            //     rb.constraints = RigidbodyConstraints2D.FreezeRotation; // Unfreeze player to allow knockback
            //     rb.linearVelocity = new Vector2(knockbackForce * direction, knockbackUpwards);
            //
            //     // Try to disable common player movement scripts during stun (if present)
            //     var playerController = collision.GetComponent("PlayerController") as MonoBehaviour;
            //
            //
            //     anim = collision.GetComponent<Animator>();
            //     anim.SetBool("isStunned", true);
            //
            // }
            Destroy(gameObject);
        }

        // if (collision.CompareTag("Ground"))
        // {
        // Destroy(gameObject);
        // }


}

}