using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]
public class MovementController : MonoBehaviour
{
    [Header("Player Movement Setting")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpHeight = 1.5f;
    [SerializeField] float gravityValue = -15f;
    [SerializeField] float stickToGroundForce = -2f;

    PlayerStats stats;
    CharacterController controller;

    Vector2 moveInput;

    // How fast the player moves up (+) or down (-)
    float verticalVelocity;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
        stats = GetComponent<PlayerStats>();

        UnityEngine.Debug.Log($"Player Stats initialized: {stats.Health}/{stats.maxHealth}.");
    }

    // Update is called once per frame
    void Update()
    {
        // Stops accumulating vertical velocity when the player is grounded
        if (controller.isGrounded && verticalVelocity < 0f)
        {
            verticalVelocity = stickToGroundForce;
        }

        // Apply gravity
        verticalVelocity += gravityValue * Time.deltaTime;

        Vector3 horizontalMovement = new Vector3(moveInput.x, 0f, moveInput.y) * moveSpeed;
        Vector3 verticalMovement = new Vector3(0f, verticalVelocity, 0f);

        controller.Move((horizontalMovement + verticalMovement) * Time.deltaTime);
    }

    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    private void OnJump(InputValue value)
    {
        if (value.isPressed && controller.isGrounded) {
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravityValue);
        }
    }

    public void ResetVerticalVelocity()
    {
        verticalVelocity = 0f;
    }
}
