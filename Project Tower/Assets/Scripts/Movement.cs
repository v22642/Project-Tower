using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody2D rb;

    private bool isGrounded;
    private float groundRadius = 0.2f;
    public Transform groundCheck;
    public LayerMask groundMask;

    private float move;
    public float speed = 10f;

    public float jumpForce = 10f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundMask);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        move = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(move * speed, rb.velocity.y);
    }

    private void Flip()
    {

    }
}
