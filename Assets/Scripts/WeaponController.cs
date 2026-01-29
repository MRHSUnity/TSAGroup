using UnityEngine;

public class WeaponController : MonoBehaviour
{
    private Animator anim;
    private GameObject player;
    private Whip whip;
    private Rigidbody2D rb;
    public enum WeaponType
    {
        Whip,
        Sword,
        Bow
    }

    public WeaponType weapon;
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
            if (weapon == WeaponType.Whip)
            {
                anim.SetBool("attackWhip", true);
                rb.linearVelocity = Vector2.zero;
                rb.constraints = RigidbodyConstraints2D.FreezeAll;

            }
        }
    }

    public void attack()
    {
        if (weapon == WeaponType.Whip)
        {
            whip.attacky();
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
}
