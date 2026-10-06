using Unity.VisualScripting;
using UnityEngine;

public class LaserMovement : MonoBehaviour
{
    [Header("Laser Movement Settings")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] Vector3 moveDirection = Vector3.back;

    [Header("Despawn Settings")]
    // Object despawns after this time in seconds
    [SerializeField] float maxLifeTime = 20f;

    float timeAlive = 0f;

    // Initializes laser movement direction and speed
    public void Initialize(Vector3 direction, float speed)
    {
        moveDirection = direction.normalized;
        moveSpeed = speed;
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        timeAlive += Time.deltaTime;
        if (timeAlive >= maxLifeTime)
        {
            Destroy(gameObject);
        }
    }

    // Destroys the laser when it triggers the despawn zone
    private void OnTriggerEnter(Collider other)
    {
        UnityEngine.Debug.Log("Laser entered DespawnZone");
        if (other.CompareTag("DespawnZone"))
        {
            Destroy(gameObject);
            UnityEngine.Debug.Log("Laser destroyed");
        }
    }
}
