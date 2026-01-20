using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;

    public float spd;
    public float jumpForce;
    public LayerMask groundLayer;
    private bool grounded;
    public Transform feetPosition;
    public float groundChackCicrle;

    public float jumpTime = 0.35f;
    public float jumpTimeCounter;
    private bool isJumping;
    
    public float input;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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

        // if (Input.GetButtonUp("Jump"))
        // {
        //     isJumping = false;
        // }
        
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(input * spd, rb.linearVelocity.y);
    }
}
