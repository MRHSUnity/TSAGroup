using System.Collections;
using UnityEngine;

public class SkeleAttack : MonoBehaviour
{
    public int damage = 3;
    public LayerMask player;

    public float konckbackTime = 0.5f;
    public float hitDirectionForce = 5f;
    public float constForce = 2f;
    public float inputForce = 7.5f;
    
    private bool knockingBack = false;
    public IEnumerator KnockBack(Rigidbody2D rb,Vector2 hitDirection, Vector2 constantForceDirection, float inputDirection)
    {
        knockingBack = true;
        Vector2 hitForce;
        Vector2 constantForce;
        Vector2 knockbackForce;
        Vector2 combinedForce;

        hitForce = hitDirection * hitDirectionForce;
        constantForce = constantForceDirection * constForce;
        
        float elapsedTime = 0f;
        while (elapsedTime < konckbackTime)
        {
            elapsedTime += Time.fixedDeltaTime;
            
            knockbackForce = constantForce * hitForce;

            if (inputDirection != 0)
            {
                combinedForce= knockbackForce + new Vector2(inputDirection * inputForce, 0);
            }
            else
            {
                combinedForce = knockbackForce;
            }

            rb.linearVelocity = combinedForce;
        }

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
           Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
           
           
       }
   }
}
