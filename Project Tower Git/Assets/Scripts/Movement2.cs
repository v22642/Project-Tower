using UnityEngine;

public class Movement2 : MonoBehaviour
{
    [Header("Object References")]
    public Rigidbody2D rb;

    [Header("Horizontal Movement")]
    public float moveSpeed = 10f;
    public float runSpeed = 10f;

    [Header("Vertical Movement")]
    public float jumpForce = 10f;
    bool isJumped = false;

    [Header("Grounded")]
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundRadius = 0.2f;
    bool isGrounded = false;

    [Header("Wall Jump")]
    public float wallJumpDelay = 0.3f;
    public float wallJumpTime = 0.2f;
    public float wallSlideSpeed = 0.3f;
    public float wallDistance = 0.5f;
    bool isWallSliding = false;
    RaycastHit2D wallCheckHit;
    float jumpTime;

    private bool isClimbing;
    private float inputVertical;
    public float climbSpeed = 8f;
    public float ladderCheckDistance;
    public LayerMask whatIsLadder;

    //private variables
    private float mx = 0f;
    bool isFacingRight = true;
    bool isWallJumped;

    private void Update()
    {
        if (!PlayerHealth.death)
        {
            if (isGrounded && Input.GetKeyDown(KeyCode.Space) && !isJumped)
            {
                Jump();
                isJumped = true;
                Invoke(nameof(SetJumpedToFalse), wallJumpTime);
            }

            if (isWallSliding && Input.GetKeyDown(KeyCode.Space) && !isWallJumped)
            {
                Jump();
                isWallJumped = true;
                Invoke(nameof(SetWallJumpedToFalse), wallJumpDelay);
            }

            if (!isWallSliding)
            {
                isWallJumped = false;
            }

            RaycastHit2D ladderHit = Physics2D.Raycast(transform.position, Vector2.up, ladderCheckDistance, whatIsLadder);

            if (ladderHit.collider != null)
            {
                Debug.Log("Ladder detected");
                if (Input.GetKey(KeyCode.W))
                {
                    isClimbing = true;
                }
            }
            else
            {
                isClimbing = false;
            }

            if (isClimbing == true)
            {
                inputVertical = Input.GetAxisRaw("Vertical");
                rb.velocity = new Vector2(rb.position.x, inputVertical * climbSpeed);
                rb.gravityScale = 0;
            }
            else
            {
                rb.gravityScale = 1;
            }
        }
    }

    private void FixedUpdate()
    {
        mx = Input.GetAxis("Horizontal");

        if (!PlayerHealth.death)
        {
            if (mx < 0)
            {
                isFacingRight = false;
                //transform.localScale = new Vector2(-1, transform.localScale.y);
            }
            else if (mx > 0)
            {
                isFacingRight = true;
                //transform.localScale = new Vector2(1, transform.localScale.y);
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                rb.velocity = new Vector2(mx * runSpeed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(mx * moveSpeed, rb.velocity.y);
            }

            bool touchingGround = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);

            if (touchingGround)
            {
                isGrounded = true;
                jumpTime = Time.time + wallJumpTime;
            }
            else if (jumpTime < Time.time)
            {
                isGrounded = false;
            }

            //wall jump

            if (isFacingRight)
            {
                wallCheckHit = Physics2D.Raycast(transform.position, new Vector2(wallDistance, 0), wallDistance, groundLayer);
                Debug.DrawRay(transform.position, new Vector2(wallDistance, 0), Color.blue);
            }
            else
            {
                wallCheckHit = Physics2D.Raycast(transform.position, new Vector2(-wallDistance, 0), wallDistance, groundLayer);
                Debug.DrawRay(transform.position, new Vector2(-wallDistance, 0), Color.blue);
            }

            if (wallCheckHit && !isGrounded && mx != 0)
            {
                isWallSliding = true;
                jumpTime = Time.time + wallJumpTime;
            }
            else if (jumpTime < Time.time)
            {
                isWallSliding = false;
            }

            if (isWallSliding)
            {
                rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, wallSlideSpeed, float.MaxValue));
            }
        }
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    void SetWallJumpedToFalse()
    {
        isWallJumped = false;
    }

    void SetJumpedToFalse()
    {
        isJumped = false;
    }
}
