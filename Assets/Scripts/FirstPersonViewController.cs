using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonViewController : MonoBehaviour
{
    [SerializeField] Transform playerCamera;    // Player Camera Object

    [Header("View Settings")]
    [SerializeField] float sensX = 0.03f;
    [SerializeField] float sensY = 0.03f;
    [SerializeField] float maxViewAngle = 85f;

    float xRotation;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Stops camera rotation when the game is paused or mouse input is unavailable
        if (Time.timeScale == 0f || Mouse.current == null) return;

        // Gets Mouse Input
        Vector2 delta = Mouse.current.delta.ReadValue();
        float mouseX = delta.x * sensX;
        float mouseY = delta.y * sensY;

        // Rotates the player when the camera rotates horizontally (left/right)
        transform.Rotate(Vector3.up * mouseX);

        // Rotates the camera when the player rotates vertically (up/down)
        xRotation = Mathf.Clamp(xRotation - mouseY, -maxViewAngle, maxViewAngle);
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
