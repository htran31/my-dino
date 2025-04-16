using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController character;
    private Vector3 direction;
    private Vector3 initialPosition;


    public float initialGravity = 9.81f * 2f;
    public float initialJumpForce = 8f;
    public float initialStrength = 5f;
    public float initialTilt = 5f;


    public float gravity { get; private set; }
    public float jumpForce { get; private set; }
    public float strength { get; private set; }
    public float tilt { get; private set; }
    private bool canJump = false;
    private bool isPaused = false;
    // private bool isSpeedBoosted = false;
    private float speedBoostEndTime = 0f;
    private float originalGameSpeed;



    private float horizontalSpeed = 0f;
    private float acceleration = 5f;
    private float deceleration = 2f;
    private float maxSpeed = 8f;



    private void Awake()
    {
        character = GetComponent<CharacterController>();
        initialPosition = new Vector3(-5f, 0, 0);

        gravity = initialGravity;
        jumpForce = initialJumpForce;
        strength = initialStrength;
        tilt = initialTilt;
    }

    private void OnEnable()
    {
        direction = Vector3.zero;
    }

    private void Update()
    {
        if (!isPaused && GameManager.Instance.gameOver == false)
        {

            float score = GameManager.Instance.score;
            direction += Vector3.down * gravity * Time.deltaTime;

            if (character.isGrounded)
            {
                direction = Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space) ? Vector3.up * jumpForce : Vector3.down;
            }

            character.Move(direction * Time.deltaTime);

            // if (isSpeedBoosted && Time.time >= speedBoostEndTime)
            // {
            //     // Speed boost has ended, reset the game speed to its original value
            //     GameManager.Instance.gameSpeed = originalGameSpeed;
            //     isSpeedBoosted = false;
            // }

            // Check for player's score (assuming you have a GameManager script controlling the score)
            if (score >= 96)
            {
                // Reset the gravity
                gravity = 9.8f;
                if (Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space))
                {
                    direction = Vector3.up * strength;
                }
                // Apply gravity and update the position
                direction += Vector3.down * gravity * Time.deltaTime;
                transform.position += direction * Time.deltaTime;

                // Tilt the bird based on the direction
                Vector3 rotation = transform.eulerAngles;
                rotation.z = direction.y * tilt;
                transform.eulerAngles = rotation;
            }

            if (score >= 245)
            {
                direction = Vector3.zero;
                gravity = 1119.8f;

                if (Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space))
                {
                    // horizontalSpeed += acceleration * Time.deltaTime;
                    // horizontalSpeed = Mathf.Clamp(horizontalSpeed, -maxSpeed, maxSpeed);
                    direction = Vector3.right * strength;
                }
                else
                {
                    // Nếu không nhấn gì => từ từ chậm lại về bên trái
                    // horizontalSpeed -= deceleration * Time.deltaTime;
                    // horizontalSpeed = Mathf.Clamp(horizontalSpeed, -maxSpeed, maxSpeed);
                }

                // Di chuyển theo trục X
                // character.Move(new Vector3(horizontalSpeed, 0, 0) * Time.deltaTime);
                direction += Vector3.left * 2.5f;
                transform.position += direction * Time.deltaTime;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            GameManager.Instance.GameOver();
        }
    }

    public void CanJumpOnGround(bool canJumpOnGround)
    {
        canJump = canJumpOnGround;
    }

    // public void ApplySpeedBoost(float boostFactor, float duration)
    // {
    //     if (!isSpeedBoosted)
    //     {
    //          originalGameSpeed = GameManager.Instance.gameSpeed;
    //         GameManager.Instance.gameSpeed = originalGameSpeed * boostFactor;
    //         speedBoostEndTime = Time.time + duration;
    //         isSpeedBoosted = true;
    //     }
    // }

    // Method to pause/unpause the player
    public void Pause()
    {
        isPaused = true; // Set the isPaused flag to true to pause the player
    }

    public void CheckOutOfBounds()
    {
        Vector3 viewportPos = Camera.main.WorldToViewportPoint(transform.position);

        // If player out of camera frame follow x-axis or y-axis
        if (viewportPos.x < 0 || viewportPos.x > 1 || viewportPos.y < 0 || viewportPos.y > 1)
        {
            GameManager.Instance.GameOver();
        }
    }


    public void ResetPlayer()
    {
        // Reset the player's position to the initial position
        transform.position = initialPosition;
        transform.rotation = Quaternion.Euler(Vector3.zero);

        // Reset any other variables or states as needed
        direction = Vector3.zero;
        isPaused = false; // Ensure the player is not paused
        gravity = initialGravity;
    }
}

