using UnityEngine;

public class OrbitProjectile2D : MonoBehaviour
{
    [Header("Homing Settings")]
    public float speed = 5f;
    public float homingDuration = 0.2f;
    public float lifetime = 5f;

    private Transform target;
    private float homingTimer = 0f;
    private float lifeTimer = 0f;
    private bool launched = false;

    private Vector2 lastDirection;

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

        // Default direction in case target is missing
        lastDirection = transform.up;
    }

    void Update()
    {
        if (!launched) return;

        lifeTimer += Time.deltaTime;

        if (lifeTimer >= lifetime)
        {
            Destroy(gameObject);
            return;
        }

        homingTimer += Time.deltaTime;

        if (homingTimer <= homingDuration && target != null)
        {
            lastDirection = (target.position - transform.position).normalized;
        }

        // Always move using last saved direction
        transform.position += (Vector3)(lastDirection * speed * Time.deltaTime);
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

            Destroy(gameObject);
        }
    }
}