using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;          // Initial jump force
    public float maxHoldJumpTime = 0.5f;   // Maximum time to hold for higher jump
    public float maxJumpForce = 15f;       // Maximum jump force for a higher jump
    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isChargingJump;
    private float jumpHoldTimer;

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
        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // Start charging the jump on button down
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            StartChargingJump();
        }

        // Continue charging the jump while button is held
        if (Input.GetButton("Jump") && isChargingJump)
        {
            ChargeJump();
        }

        // Execute jump on button release
        if (Input.GetButtonUp("Jump") && isChargingJump)
        {
            ExecuteJump();
        }
    }

    private void HandleTouchControls()
    {
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                float screenWidth = Screen.width;

                // Left side of the screen for left-right movement
                if (touch.position.x < screenWidth / 2)
                {
                    float direction = touch.position.x < screenWidth / 4 ? -1 : 1; // left or right within the left side
                    MoveHorizontal(direction);
                }
                // Right side of the screen for jump
                else if (touch.phase == TouchPhase.Began && isGrounded)
                {
                    StartChargingJump();
                }

                if (touch.phase == TouchPhase.Stationary && isChargingJump)
                {
                    ChargeJump();
                }

                if (touch.phase == TouchPhase.Ended && isChargingJump)
                {
                    ExecuteJump();
                }
            }
        }
    }

    private void MoveHorizontal(float direction)
    {
        rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocity.y);
    }

    private void StartChargingJump()
    {
        isChargingJump = true;
        jumpHoldTimer = 0f; // Reset timer when jump starts
    }

    private void ChargeJump()
    {
        if (jumpHoldTimer < maxHoldJumpTime)
        {
            jumpHoldTimer += Time.deltaTime;
        }
    }

    private void ExecuteJump()
    {
        isChargingJump = false;

        // Calculate jump force based on hold time
        float calculatedJumpForce = Mathf.Lerp(jumpForce, maxJumpForce, jumpHoldTimer / maxHoldJumpTime);
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, calculatedJumpForce);
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
