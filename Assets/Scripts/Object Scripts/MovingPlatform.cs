using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    enum StartDirection { Left, Right }

    [Header("Platform Movement Settings")]
    [SerializeField] StartDirection startDirection = StartDirection.Right;
    [SerializeField] float xOffset = 5f;
    [SerializeField] float moveSpeed = 3f;

    Vector3 startPosition;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the offset based on the sine wave and the starting direction
        float directionMultiplier = (startDirection == StartDirection.Right) ? 1f : -1f;

        // Calculate the current offset using a sine wave for smooth back-and-forth movement
        float currentOffset = Mathf.Sin(Time.time * moveSpeed) * xOffset * directionMultiplier;

        // Update the platform's position based on the calculated offset
        transform.localPosition = new Vector3(startPosition.x + currentOffset, startPosition.y, startPosition.z);
    }
}
