using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Flashlight : MonoBehaviour
{
    [Header("Flashlight Settings")]
    [SerializeField] private Light spotlight;
    [SerializeField] private InputActionProperty toggleAction;

    [Header("Layer Masks")]
    [SerializeField] private LayerMask hiddenMask;
    [SerializeField] private LayerMask obstacleMask;

    // Stores objects already revealed so we don't repeatedly call Reveal().
    private readonly HashSet<HiddenObject> revealedObjects = new();

    private void Start()
    {
        spotlight.enabled = false; // Start with the flashlight off
    }

    // Update is called once per frame
    void Update()
    {
        if (spotlight == null || !spotlight.enabled)
            return;

        DetectHiddenObjects();
    }

    void OnEnable()
    {
        toggleAction.action?.Enable();
        toggleAction.action.performed += OnToggleFlashlight;
    }

    void OnDisable()
    {
        toggleAction.action?.Disable();
        toggleAction.action.performed -= OnToggleFlashlight; 
    }

    private void OnToggleFlashlight(InputAction.CallbackContext ctx)
    {
        if (spotlight != null)
            spotlight.enabled = !spotlight.enabled;
    }

    private void DetectHiddenObjects()
    {
        Vector3 origin = spotlight.transform.position;

        // Find all colliders within the spotlight's range that are on the hidden layer.
        Collider[] candidates = Physics.OverlapSphere(
            origin,
            spotlight.range,
            hiddenMask,
            QueryTriggerInteraction.Collide
        );

        // Iterate through each candidate collider to determine if it should be revealed.
        foreach (Collider col in candidates)
        {
            if (col == null)
                continue;

            // Get the HiddenObject component from the collider's parent.
            HiddenObject hidden =
                col.GetComponentInParent<HiddenObject>();

            if (hidden == null || revealedObjects.Contains(hidden))
                continue;

            Vector3 target = col.bounds.center;
            Vector3 offset = target - origin;
            float distance = offset.magnitude;

            if (distance <= 0.001f)
            {
                hidden.Reveal();
                revealedObjects.Add(hidden);
                continue;
            }

            Vector3 direction = offset / distance;

            // Checks if the hidden object is within the spotlight's cone angle.
            float angle = Vector3.Angle(
                spotlight.transform.forward,
                direction
            );

            if (angle > spotlight.spotAngle * 0.5f)
                continue;

            // Checks if there are any obstacles between the flashlight and the hidden object.
            if (Physics.Raycast(
                origin,
                direction,
                out RaycastHit hit,
                distance + 0.05f,
                obstacleMask | hiddenMask,
                QueryTriggerInteraction.Ignore
            ))
            {
                HiddenObject hitObject =
                    hit.collider.GetComponentInParent<HiddenObject>();

                // If the raycast hit the hidden object, reveal it.
                if (hitObject == hidden)
                {
                    hidden.Reveal();
                    revealedObjects.Add(hidden);
                }
            }
        }
    }
}
