using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController character;
    private Vector3 direction;
     private Vector3 initialPosition;

    public float gravity = 9.81f * 2f;
    public float jumpForce = 8f;
    private bool canJump = false;
    private bool isPaused = false; 

    // private bool isSpeedBoosted = false;
    private float speedBoostEndTime = 0f;
    private float originalGameSpeed;


public float strength = 5f;
public float tilt = 5f;

    private void Awake()
    {
        character = GetComponent<CharacterController>();
        initialPosition = transform.position;
    }

    private void OnEnable()
    {
        direction = Vector3.zero;
    }

    private void Update() 
    {
        
        if (!isPaused)
        {
        direction += Vector3.down * gravity * Time.deltaTime;

        if (character.isGrounded)
        {
            direction = Vector3.down;
            if (Input.GetButton("Jump")  )
            {
                direction = Vector3.up * jumpForce;
            }
        }

        character.Move(direction * Time.deltaTime);

        // if (isSpeedBoosted && Time.time >= speedBoostEndTime)
        // {
        //     // Speed boost has ended, reset the game speed to its original value
        //     GameManager.Instance.gameSpeed = originalGameSpeed;
        //     isSpeedBoosted = false;
        // }

        // Check for player's score (assuming you have a GameManager script controlling the score)
        if (GameManager.Instance.score >= 50)
        {
            // Reset the gravity
            gravity = 9.8f;
            if (Input.GetButton("Jump")  )
            {
                direction = Vector3.up * strength;
            }
            if (Input.touchCount > 0) {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began) {
                    direction = Vector3.up * strength;
                }
            }
            // Apply gravity and update the position
        direction +=  Vector3.down *gravity * Time.deltaTime;
        transform.position += direction * Time.deltaTime;

        // Tilt the bird based on the direction
        Vector3 rotation = transform.eulerAngles;
        rotation.z = direction.y * tilt;
        transform.eulerAngles = rotation;
        }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Obstacle")) {
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
        // Optionally, you can stop any player movement or animations here
    }
    
    public void ResetPlayer()
    {
        // Reset the player's position to the initial position
        transform.position = initialPosition;
        
        // Reset any other variables or states as needed
        direction = Vector3.zero;
        isPaused = false; // Ensure the player is not paused
    }
}

