using UnityEngine;

public class LaserDamageTrigger : MonoBehaviour
{
    [Header("Laser Damage Setting")]
    [SerializeField] int minDamageAmount = 5;
    [SerializeField] int maxDamageAmount = 15;
    [SerializeField] float damageInterval = 0.2f;

    float nextDamageTime;

    void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (Time.time < nextDamageTime) return;

        PlayerStats stats = other.GetComponent<PlayerStats>();
        if (stats == null) return;

        int damageAmount = Random.Range(minDamageAmount, maxDamageAmount + 1);
        stats.TakeDamage(damageAmount);
        nextDamageTime = Time.time + damageInterval;

        UnityEngine.Debug.Log($"The Player took {damageAmount} damage.");
        
    }
}
