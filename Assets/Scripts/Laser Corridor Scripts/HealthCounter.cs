using TMPro;
using UnityEngine;

public class HealthCounter : MonoBehaviour
{
    [SerializeField] PlayerStats playerStat;

    [SerializeField] TMP_Text healthText;

    // Update is called once per frame
    void Update()
    {
        healthText.color = playerStat.Health <= 0 ? Color.red : Color.white;
        healthText.text = $"HP: {playerStat.Health}/{playerStat.maxHealth}";
    }
}
