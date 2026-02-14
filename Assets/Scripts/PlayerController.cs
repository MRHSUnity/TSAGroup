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
    public float groundCheckCicrle;
    public float gravity;
    private bool facingRight = true;
    
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


    }
  
    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(input * spd, rb.linearVelocity.y);
    }
    // Update is called once per frame
    void Update()
    {
       input = Input.GetAxisRaw("Horizontal");
       grounded = Physics2D.OverlapCircle(feetPosition.position, groundCheckCicrle, groundLayer);
       if (grounded && rb.linearVelocity.y <= 0f)
       {
           isJumping = false;
           anim.SetBool("isJumping", false);
       }
       
       // tuning: variable jump height (short tap vs longer hold)
       // make jumps and falls slower by reducing multipliers and initial impulse
       float upSpeedFactor = 0.6f;        // slightly lower initial impulse
       float fallMultiplier = 1.6f;         // slower, less snappy fall
       float lowJumpMultiplier = 1.3f;    // less aggressive shortening when released early
       
       // Single jump: only allow jump when grounded
       if (Input.GetButtonDown("Jump") && grounded)
       {
           anim.SetBool("isJumping", true);
           isJumping = true;
           jumpTimeCounter = jumpTime;
           float startY = jumpForce * upSpeedFactor;
           rb.linearVelocity = new Vector2(rb.linearVelocity.x, startY);
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
       
       // when jump button is released, stop the hold window
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
        if(input >.1f && !facingRight && !anim.GetBool("attackWhip"))
        {
            flip();
            facingRight = true;
        }
        if(input < -.1f && facingRight && !anim.GetBool("attackWhip"))
        {
            flip();
            facingRight = false;
        }
        if (input > .1f || input < -.1f)
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }
        
    }
    private void flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
    

}