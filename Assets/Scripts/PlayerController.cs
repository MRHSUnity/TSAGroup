using System;
using NUnit.Framework.Constraints;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;

    public float spd;
    public float jumpForce;
    public LayerMask groundLayer;
    private bool grounded;
    public Transform feetPosition;
    public float groundChackCicrle;

    public HealthBarUI healthBarUI;
    public float health, maxHealth;
    public RectTransform healthBar;
    

    public float jumpTime = 0.35f;
    public float jumpTimeCounter;
    private bool isJumping;
    
    
    
    public float input;
    private Animator anim;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        //health = 10f;
        
        healthBarUI.setMaxHealth(maxHealth);

    }
    public void setHealth(float healthChange)
    {
        health += healthChange;
        health = Mathf.Clamp(health, 0, maxHealth);
    }
    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(input * spd, rb.linearVelocity.y);

        
        
    }
    // Update is called once per frame
    void Update()
    {
        //if (input.GetKeyDown("d"))
        //{
        //    setHealth(-2f);
        //}
        //if (input.GetKeyDown("u"))
        //{
        //    setHealth(2f);
        //}
        input = Input.GetAxisRaw("Horizontal");
        grounded = Physics2D.OverlapCircle(feetPosition.position, groundChackCicrle, groundLayer);
        if (grounded == true && Input.GetButtonDown("Jump"))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.linearVelocity = Vector2.up * jumpForce;
        }

        if (Input.GetButtonDown("Jump") && isJumping == true)
        {
            if (jumpTimeCounter > 0)
            {
                rb.linearVelocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if (Input.GetButtonUp("Jump"))
        {
           isJumping = false;
        }
        if(input >.1f)
        {
            anim.SetBool("isWalkingRight", true);
        }
        else
        {
            anim.SetBool("isWalkingRight", false);
        }
        if(input  < -.1f)
        {
            anim.SetBool("isWalkingLeft", true);
        }
        else
        {
            anim.SetBool("isWalkingLeft", false);
        }
        // add facing left and right
        
    }


    // public void attack()
    // {
    //     weaponController.attack();
    // }
    // public void endAttack()
    // {
    //     weaponController.endAttack();
    // }

    private void OnDrawGizmos()
    {
        

    }
}