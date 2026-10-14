using System;
using UnityEngine;

public class IncreaseHealthTrigger : MonoBehaviour
{
    [Header("Health Increase Setting")]
    [SerializeField] int healAmount = 10;

    public event Action OnActivated;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerStats stats = other.GetComponent<PlayerStats>();
        if (stats == null || stats.Health == stats.maxHealth) return;

        stats.Heal(healAmount);
        UnityEngine.Debug.Log($"The Player healed {healAmount} HP.");
        
        OnActivated?.Invoke();
        gameObject.SetActive(false);
    }
}
