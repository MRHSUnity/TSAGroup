using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // References
    public Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public LayerMask wallLayer;

    // Movement
    public float moveSpeed = 8f;
    public float acceleration = 60f;
    public float deceleration = 80f;
    public float airControlMultiplier = 0.7f;

    // Jump
    public float jumpForce = 12f;
    public float coyoteTime = 0.1f;
    public float jumpBufferTime = 0.1f;

    // Better Gravity
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    // Celeste Wall Settings
    public Vector2 wallCheckSize = new Vector2(0.2f, 0.9f);
    public float wallSlideSpeed = 1.5f;
    public float wallJumpForceX = 16f;
    public float wallJumpForceY = 14f;
    public float wallJumpLockTime = 0.2f;

    private bool isGrounded;
    private bool isWallSliding;
    private bool isWallJumping;

    private float horizontal;
    private float coyoteCounter;
    private float jumpBufferCounter;
    private float wallJumpTimer;

    private int wallSide;            // -1 = left wall, 1 = right wall
    private int lastWallJumpSide = 0;

    private bool facingRight = true;
    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        // Ground check
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        if (isGrounded)
        {
            lastWallJumpSide = 0;
            anim.SetBool("isJumping", false);
        }

        // WALL DETECTION (stable box check)
        Collider2D hitRight = Physics2D.OverlapBox(
            transform.position + Vector3.right * 0.6f,
            wallCheckSize,
            0,
            wallLayer);

        Collider2D hitLeft = Physics2D.OverlapBox(
            transform.position + Vector3.left * 0.6f,
            wallCheckSize,
            0,
            wallLayer);

        if (hitRight != null)
            wallSide = 1;
        else if (hitLeft != null)
            wallSide = -1;
        else
            wallSide = 0;

        // Coyote time
        if (isGrounded)
            coyoteCounter = coyoteTime;
        else
            coyoteCounter -= Time.deltaTime;

        // Jump buffer
        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
            jumpBufferCounter -= Time.deltaTime;

        // Wall slide
        isWallSliding = wallSide != 0 && !isGrounded && rb.linearVelocity.y < 0;

        if (isWallSliding)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, -wallSlideSpeed);
        }

        // Jump logic
        if (jumpBufferCounter > 0)
        {
            // Normal jump
            if (coyoteCounter > 0)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                jumpBufferCounter = 0;
                anim.SetBool("isJumping", true);
            }
            // Celeste wall jump
            else if (isWallSliding)
            {
                isWallJumping = true;
                wallJumpTimer = wallJumpLockTime;

                float verticalVelocity = wallJumpForceY;

                // Prevent height stacking on same wall
                if (lastWallJumpSide == wallSide)
                {
                    verticalVelocity = Mathf.Min(rb.linearVelocity.y, wallJumpForceY);
                }

                rb.linearVelocity = new Vector2(-wallSide * wallJumpForceX, verticalVelocity);
                anim.SetBool("isJumping", true);
                
                lastWallJumpSide = wallSide;
                jumpBufferCounter = 0;
            }
        }

        // Better gravity
        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.linearVelocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        // Flip
        if (horizontal > 0.1f && !facingRight)
        {
            Flip();
            facingRight = true;
        }
        else if (horizontal < -0.1f && facingRight)
        {
            Flip();
            facingRight = false;
        }

        if (anim != null)
            anim.SetBool("isWalking", Mathf.Abs(horizontal) > 0.1f);
    }

    void FixedUpdate()
    {
        // Wall jump lock (prevents instant re-stick)
        if (isWallJumping)
        {
            wallJumpTimer -= Time.fixedDeltaTime;
            if (wallJumpTimer <= 0)
                isWallJumping = false;
            else
                return;
        }

        float targetSpeed = horizontal * moveSpeed;
        float speedDifference = targetSpeed - rb.linearVelocity.x;

        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f)
            ? acceleration
            : deceleration;

        if (!isGrounded)
            accelRate *= airControlMultiplier;

        float movement = speedDifference * accelRate * Time.fixedDeltaTime;

        rb.linearVelocity = new Vector2(rb.linearVelocity.x + movement, rb.linearVelocity.y);
    }

    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}