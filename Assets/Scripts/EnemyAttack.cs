using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public GameObject attackArea;
    private Animator anim;
    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponentInParent(typeof(Animator)) as Animator;
        rb = GetComponent<Rigidbody2D>();
    }
    private float MoveDirection
    {
        get
        {
            float vx = rb.linearVelocity.x;
            if (Mathf.Abs(vx) > 0.01f) return Mathf.Sign(vx);
            float scaleX = transform.localScale.x;
            return Mathf.Sign(scaleX == 0f ? 1f : scaleX);
        }
    }
    public void enemyAttack()
    {
        attackArea.SetActive(true);
    }

    public void endEnemyAttack()
    {
        attackArea.SetActive(false);
        anim.SetBool("isAttacking",false);
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;   
    }
    // Update is called once per frame
    void Update()
    {
        // Vector3 pos = attackArea.transform.localPosition;
        // pos.x = Mathf.Abs(pos.x) * MoveDirection;
        // attackArea.transform.localPosition = pos;    
    }
}
