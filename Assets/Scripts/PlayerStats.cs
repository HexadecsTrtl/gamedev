using System;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Player Statistics")]
    public int Health;
    public int maxHealth = 100;

    public event Action onPlayerDeath;
    bool isDead = false;

    private void Start()
    {
        Health = maxHealth;
        UnityEngine.Debug.Log($"Player initialized with HP: {Health}/{maxHealth}");
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        Health = Mathf.Clamp(Health, 0, maxHealth);
        UnityEngine.Debug.Log($"The Player took {damage} damage. HP: {Health}/{maxHealth}");

        if (Health <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        if (isDead) return;

        Health += amount;
        Health = Mathf.Clamp(Health, 0, maxHealth);
        UnityEngine.Debug.Log($"The Player was healed for {amount} HP. HP: {Health}/{maxHealth}");
    }

    public void Die()
    {
        if (isDead) return;
        isDead = true;

        UnityEngine.Debug.Log("The Player has died.");
        onPlayerDeath?.Invoke();
    }
}
