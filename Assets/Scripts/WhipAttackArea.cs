using UnityEngine;

public class WhipAttackArea : MonoBehaviour
{
    public int damage = 3;
    public LayerMask enemies;
    private Animator anim;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
   private void OnTriggerEnter2D(Collider2D collision)
   {
         if (((1 << collision.gameObject.layer) & enemies) != 0)
         {
              EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();
              anim = collision.GetComponent<Animator>();
              if (enemyHealth != null)
              {
                enemyHealth.health -= damage;
                Debug.Log("Enemy hit for " + damage + " damage.");
                anim.SetBool("isStunned", true);
              }
         }
   }
}
