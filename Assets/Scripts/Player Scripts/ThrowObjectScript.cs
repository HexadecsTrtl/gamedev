using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThrowingScript : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private InputActionProperty throwAction;

    [Header("References")]
    [SerializeField] Transform cam;
    [SerializeField] Transform atkPoint;
    [SerializeField] GameObject objectToThrow;
    [SerializeField] TMP_Text projectileCountText;

    [Header("Throw Settings")]
    [SerializeField] int totalThrows = 100;
    [SerializeField] float throwCooldown = 0.1f;

    [SerializeField] float throwForce = 10f;
    [SerializeField] float throwUpwardForce = 5f;

    bool readyToThrow;

    // Enable the input action when the object is active
    private void OnEnable()
    {
        throwAction.action?.Enable();
    }

    // Disable the input action when the object is inactive
    private void OnDisable()
    {
        throwAction.action?.Disable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        readyToThrow = true;
        UpdateProjectileText();
    }

    // Update is called once per frame
    void Update()
    {
        if (throwAction.action != null && throwAction.action.WasPressedThisFrame() && readyToThrow && totalThrows > 0)
        {
            Throw();
        }

        UpdateProjectileText();
    }

    private void Throw()
    {
        readyToThrow = false;

        // Instantiates the object to throw
        GameObject projectile = Instantiate(objectToThrow, atkPoint.position, cam.rotation);
        
        // Destroys the projectile after 5 seconds
        Destroy(projectile, 10f);

        // Gets the Rigidbody component of the projectile
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        Vector3 forceDirection = cam.forward;

        RaycastHit hit;
        if (Physics.Raycast(cam.position, cam.forward, out hit, 500f))
        {
            // Adjusts the force to add based on the distance to the hit point
            forceDirection = (hit.point - atkPoint.position).normalized;
        }

        // Calculates the force of the projectile
        Vector3 forceToAdd = forceDirection * throwForce + cam.transform.up * throwUpwardForce;

        // Applies the force to the projectile (Adds Force once)
        projectileRb.AddForce(forceToAdd, ForceMode.Impulse);

        totalThrows--;

        // Resets the throw cooldown
        Invoke(nameof(ResetThrow), throwCooldown);
    }

    private void ResetThrow()
    {
        readyToThrow = true;
    }

    private void UpdateProjectileText()
    {
        if (projectileCountText != null)
        {
            projectileCountText.text = $"Projectiles: {totalThrows}";
        }
    }

    public int GetTotalThrows()
    {
        return totalThrows;
    }
}
