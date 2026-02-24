using UnityEngine;

public class WeaponController : MonoBehaviour
{
    private Animator anim;
    private GameObject player;
    private Rigidbody2D rb;
    public MonoBehaviour movementScript;

    public enum WeaponType
    {
        Whip,
        HolyWater,
        Bow
    }

    public WeaponType weapon;
    
    private Whip whip;
    private HolyWater holyWater;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        anim = GetComponent<Animator>();
        whip = GetComponent<Whip>();
        rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {

                anim.SetBool("attack", true);
                rb.linearVelocity = Vector2.zero;
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
            
        }
    }

    public void attack()
    {
        /*
         * Whip = 1
         * Holy Water = 2
         */
        if (weapon == WeaponType.Whip)
        {
            whip.attacky();
            anim.SetInteger("weapon", 1);
        }

        if (weapon == WeaponType.HolyWater)
        {
            
        }

    }
    public void endAttack()
    {
        if (weapon == WeaponType.Whip)
        {
            whip.endAttacky();
            anim.SetBool("attackWhip",false);
            
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
    public void beginStun()
    {
        movementScript.enabled= false;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    public void endStun()
    {
        rb.linearVelocity = Vector2.zero;
        movementScript.enabled = true;
        anim.SetBool("isStunned", false);
        anim.SetBool("attackWhip", false);
    }
}
