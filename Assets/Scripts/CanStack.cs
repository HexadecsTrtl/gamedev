using System;
using UnityEngine;

public class CanStack : MonoBehaviour
{
    [Header("Stack Contents")]
    [SerializeField] Transform[] cans = new Transform[3];
    [Header("Knockdown Thresholds")]
    [SerializeField] float positionThreshold = 0.5f;
    [SerializeField] float angleThreshold = 30f;

    private Vector3[] initialLocalPositions;
    private Quaternion[] initialLocalRotations;

    public bool IsStackKnockedOver { get; set; } = false;

    // Event that notifies when the stack is knocked over
    public static event Action OnStackKnockedOver;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialLocalPositions = new Vector3[cans.Length];
        initialLocalRotations = new Quaternion[cans.Length];

        // Store the initial positions and rotations of each can
        for (int i = 0; i < cans.Length; i++)
        {
            if (cans[i] != null)
            {
                initialLocalPositions[i] = cans[i].localPosition;
                initialLocalRotations[i] = cans[i].localRotation;
            }
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (IsStackKnockedOver) return;

        // Check each can for displacement or tilt
        for (int i = 0; i < cans.Length; i++)
        {
            if (cans[i] == null) continue;

            // Check the distance moved from original spot
            float distanceMoved = Vector3.Distance(cans[i].localPosition, initialLocalPositions[i]);

            // Check the tilt angle change
            float angleTilted = Quaternion.Angle(cans[i].localRotation, initialLocalRotations[i]);

            // If either threshold is exceeded, mark the stack as knocked over
            if (distanceMoved > positionThreshold || angleTilted > angleThreshold)
            {
                MarkAsKnockedOver();
                break;
            }
        }
    }

    private void MarkAsKnockedOver()
    {
        IsStackKnockedOver = true;
        OnStackKnockedOver?.Invoke();
    }
}
