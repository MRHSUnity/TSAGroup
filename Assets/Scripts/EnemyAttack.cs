using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public GameObject attackArea;
    private Animator anim;
    private Rigidbody2D rb;
    private RigidbodyConstraints2D rbConstraints;
    
    public MonoBehaviour movementScript => GetComponent<EnemyHealth>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponentInParent(typeof(Animator)) as Animator;
        rb = GetComponent<Rigidbody2D>();
        rbConstraints = rb.constraints;
    }
  
    public void enemyAttack()
    {
        attackArea.SetActive(true);
    }

    public void endEnemyAttack()
    {
        attackArea.SetActive(false);
        anim.SetBool("isAttacking",false);
        rb.constraints = rbConstraints;
    }
    public void Stun()
    {
        rb.linearVelocity = Vector2.zero;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }
    public void EndStun()
    {
        rb.constraints = rbConstraints;
        anim.SetBool("isStunned", false);

    }
    // Update is called once per frame
    void Update()
    {
        // Vector3 pos = attackArea.transform.localPosition;
        // pos.x = Mathf.Abs(pos.x) * MoveDirection;
        // attackArea.transform.localPosition = pos;    
    }
}
