using UnityEngine;

public class LaserSpawner : MonoBehaviour
{
    [Header("Object to spawn")]
    [SerializeField] GameObject[] laserPrefabs;

    [Header("Spawn Interval Settings")]
    [SerializeField] float minSpawnInterval = 1f;
    [SerializeField] float maxSpawnInterval = 3f;

    [Header("Laser Speed Range")]
    [SerializeField] float minSpeed = 3f;
    [SerializeField] float maxSpeed = 8f;

    [Header("Direction")]
    [SerializeField] Transform corridorEnd;

    float timer, nextSpawnTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ScheduleNextSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= nextSpawnTime)
        {
            SpawnLaser();
            timer = 0f;
            ScheduleNextSpawn();
        }
    }

    private void ScheduleNextSpawn()
    {
        nextSpawnTime = Random.Range(minSpawnInterval, maxSpawnInterval);
    }

    private void SpawnLaser()
    {
        if (laserPrefabs == null || laserPrefabs.Length == 0)
        {
            UnityEngine.Debug.LogWarning("LaserSpawnerScript: no laser prefabs assigned.", this);
            return;
        }

        GameObject obj = laserPrefabs[Random.Range(0, laserPrefabs.Length)];
        GameObject laser = Instantiate(obj, transform.position, obj.transform.rotation);

        // Determines the direction of the laser based on the corridor end position
        Vector3 direction = (corridorEnd != null)
            ? (corridorEnd.position - transform.position).normalized 
            : transform.forward;

        float moveSpeed = Random.Range(minSpeed, maxSpeed);

        LaserMovement movement = laser.GetComponent<LaserMovement>();
        if (movement != null)
        {
            movement.Initialize(direction, moveSpeed);
        }
        else
        {
            UnityEngine.Debug.LogWarning("LaserSpawnerScript: object has no LaserMovement component.", this);
        }
    }
}
