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
    public float gravity;
    
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
            
        // tuning: variable jump height (short tap vs longer hold)
        // make jumps and falls slower by reducing multipliers and initial impulse
        float upSpeedFactor = 0.85f;        // slightly lower initial impulse
        float fallMultiplier = 1.3f;       // slower, less snappy fall
        float lowJumpMultiplier = 1.3f;    // less aggressive shortening when released early
        
        // manage double jump using a per-instance PlayerPrefs key (persists across frames)
        string jumpKey = "jumpsLeft_" + GetInstanceID();
        // reset jumps when grounded
        if (grounded)
        {
            PlayerPrefs.SetInt(jumpKey, 2);
        }
        
        // Jump start: allow jump if grounded or if we have extra jumps remaining
        if (Input.GetButtonDown("Jump"))
        {
            int jumpsLeft = PlayerPrefs.GetInt(jumpKey, 2);
            if (grounded || jumpsLeft > 0)
        { 
            isJumping = true; 
            jumpTimeCounter = jumpTime; 
            float startY = jumpForce * upSpeedFactor;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, startY);
        
                // consume one jump if not grounded (for initial ground jump we still decrement to allow one mid-air)
                jumpsLeft = Mathf.Max(jumpsLeft - 1, 0);
                PlayerPrefs.SetInt(jumpKey, jumpsLeft);
            }
        }
            
        // keep global gravity based on configured value
        Physics2D.gravity = new Vector2(0, -gravity);
            
        // allow variable jump height by a short hold window
        if (Input.GetButton("Jump") && isJumping)
        {
            if (jumpTimeCounter > 0f)
            { 
                // still within hold window: consume time, let default gravity be applied
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
            
        // apply additional gravity when falling or when jump was released early
        if (rb.linearVelocity.y < 0f)
        { 
            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x,
                rb.linearVelocity.y + Physics2D.gravity.y * (fallMultiplier - 1f) * Time.deltaTime
            );
        }
        else if (rb.linearVelocity.y > 0f && !isJumping)
        { 
            // released early while moving up -> shorten jump
            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x,
                rb.linearVelocity.y + Physics2D.gravity.y * (lowJumpMultiplier - 1f) * Time.deltaTime
            );
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