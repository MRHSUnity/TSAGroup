using NUnit.Framework.Constraints;
using UnityEngine;

public class sensor : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public LayerMask player;
    public Animator anim;
    public Rigidbody2D rb;
    
    void Start()
    {
        anim = GetComponentInParent<Animator>();
        rb = GetComponentInParent<Rigidbody2D>();
    }

  private void OnTriggerStay2D(Collider2D collision)
  {
      if ((((1 << collision.gameObject.layer) & player) != 0)&&!anim.GetBool("isAttacking"))
      {
          anim.SetBool("isAttacking", true);
          rb.linearVelocity = Vector2.zero;
          rb.constraints = RigidbodyConstraints2D.FreezeAll;
      }
          
      
  }

}
