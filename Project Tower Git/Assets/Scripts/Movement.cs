using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody2D rb;

    private bool isGrounded;
    public float checkRadius = 0.2f;
    public Transform groundCheck;
    public LayerMask groundMask;

    private float move;
    public float speed;
    private bool isFacingRight;

    public float jumpForce = 10f;
    private int extraJumps;
    public int extraJumpsValue;

    bool isTouchingFront;
    public Transform frontCheck;
    bool wallSliding;
    public float wallSlidingSpeed;

    bool wallJumping;
    public float xWallForce;
    public float yWallForce;
    public float wallJumpTime;

    public float climbTime;
    public float climbSpeed;
    public float frontCheckRadius;
    private float climbSpeedSave;
    public bool godClimb;


    private void Start()
    {
        extraJumps = extraJumpsValue;
        rb = GetComponent<Rigidbody2D>();
        climbSpeedSave = climbSpeed;
    }

    private void Update()
    {
        if (!PlayerHealth.death)
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundMask);

            if (isGrounded)
                extraJumps = extraJumpsValue;

            if (Input.GetKeyDown(KeyCode.Space) && extraJumps > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                extraJumps--;
            }
            else if (Input.GetKeyDown(KeyCode.Space) && extraJumps == 0 && isGrounded)
            {
                rb.velocity = Vector2.up * jumpForce;
            }

            if (move > 0 && isFacingRight)
            {
                Flip();
            }
            else if (move < 0 && !isFacingRight)
            {
                Flip();
            }

            isTouchingFront = Physics2D.OverlapCircle(frontCheck.position, frontCheckRadius, groundMask);

            if (isTouchingFront && !isGrounded && move != 0)
            {
                if (climbSpeed > 0)
                {
                    rb.velocity = Vector2.up * climbSpeed;
                }
                else
                    wallSliding = true;

                if (!godClimb)
                    Invoke(nameof(SetClimbToZero), climbTime);
            }
            else
            {
                wallSliding = false;
            }

            if (isGrounded)
                climbSpeed = climbSpeedSave;

            if (wallSliding)
            {
                rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
            }

            //------------------------------------WALL JUMP (FUCKED)---------------------------------------------------
            //if (Input.GetKeyDown(KeyCode.Space) && wallSliding)
            //{
            //    wallJumping = true;
            //    Invoke(nameof(SetWallJumpingToFalse), wallJumpTime);
            //}

            //if (wallJumping)
            //{
            //    rb.velocity = new Vector2(xWallForce * -move, yWallForce);
            //}
        }
    }

    private void FixedUpdate()
    {
        if (!PlayerHealth.death)
        {
            move = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(move * speed, rb.velocity.y);
        }
    }

    public void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void SetWallJumpingToFalse()
    {
        wallJumping = false;
    }

    void SetClimbToZero()
    {
        climbSpeed = 0;
    }
}