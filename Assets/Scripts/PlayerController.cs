using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    private Rigidbody2D rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        #if UNITY_EDITOR // Use keyboard controls for testing in Unity editor
        HandleKeyboardControls();
        #else // Use touch controls on mobile
            HandleTouchControls();
        #endif
    }

    private void HandleKeyboardControls()
    {
        // For testing in editor
        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    private void HandleTouchControls()
    {
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Stationary)
                {
                    float screenWidth = Screen.width;

                    // Left side of the screen for left-right movement
                    if (touch.position.x < screenWidth / 2)
                    {
                        float direction = touch.position.x < screenWidth / 4 ? -1 : 1; // left or right within the left side
                        MoveHorizontal(direction);
                    }
                    // Right side of the screen for jump
                    else
                    {
                        Jump();
                    }
                }
            }
        }
    }

    private void MoveHorizontal(float direction)
    {
        rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocity.y);
    }

    private void Jump()
    {
        if (isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
