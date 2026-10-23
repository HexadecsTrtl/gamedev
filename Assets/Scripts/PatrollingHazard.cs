using UnityEngine;

public class GuardHazard : Hazard
{
    [Header("Patrol Settings")]
    [SerializeField] Transform[] waypoints;
    [SerializeField] float speed = 12f;
    [SerializeField] float turnSpeed = 7f;
    int index;

    [Header("Contact Settings")]
    [SerializeField] float contactRadius = 0.8f;

    [Header("Vision Settings")]
    [SerializeField] Transform eye;
    [SerializeField] float viewDistance = 10f;
    [SerializeField, Range(1, 180)] float viewAngle = 70f;

    void Update()
    {
        Patrol();

        if (Physics.CheckSphere(transform.position, contactRadius, playerMask))
            Catch("You were caught by the guard!");
        else if (CanSeePlayer())
            Catch("You were spotted by the guard!");
    }

    void Patrol()
    {
        if (waypoints.Length == 0) return;

        // Move towards the current waypoint
        Vector3 dir = waypoints[index].position - transform.position;
        dir.y = 0;

        if (dir.magnitude < 0.1f)
        {
            index = (index + 1) % waypoints.Length;
            return;
        }

        // Move and rotate towards the waypoint
        transform.position += dir.normalized * speed * Time.deltaTime;
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            Quaternion.LookRotation(dir),
            turnSpeed * Time.deltaTime);
    }

    bool CanSeePlayer()
    {
        // Is the player within the view distance?
        Collider[] hits = Physics.OverlapSphere(eye.position, viewDistance, playerMask);
        if (hits.Length == 0) return false;

        Vector3 target = hits[0].bounds.center;
        Vector3 toPlayer = target - eye.position;

        // Is the player within the view angle?
        if (Vector3.Angle(eye.forward, toPlayer) > viewAngle * 0.5f) return false;

        // Is there an obstacle between the guard and the player?
        if (Physics.Raycast(eye.position, toPlayer.normalized, out RaycastHit hit,
                            viewDistance, obstacleMask | playerMask))
            return ((1 << hit.collider.gameObject.layer) & playerMask) != 0;

        return false;
    }

    // Draw gizmos to visualize the contact radius and vision cone
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, contactRadius);

        if (!eye) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(eye.position, Quaternion.Euler(0, -viewAngle / 2, 0) * eye.forward * viewDistance);
        Gizmos.DrawRay(eye.position, Quaternion.Euler(0, viewAngle / 2, 0) * eye.forward * viewDistance);
    }
}