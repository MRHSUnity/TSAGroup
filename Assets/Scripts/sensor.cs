using NUnit.Framework.Constraints;
using UnityEngine;

public class sensor : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public LayerMask player;
    private Animator anim;
    private Rigidbody2D rb;
    public bool working = false;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

  private void OnTriggerEnter2D(Collider2D collision)
  {
  
      if (((1 << collision.gameObject.layer) & player) != 0)
      {
          anim.SetBool("isAttacking", true);
          rb.linearVelocity = Vector2.zero;
          rb.constraints = RigidbodyConstraints2D.FreezeAll;
          working = true;
      }
  }
    // Update is called once per frame

}
