using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] TMP_Text EndScreenMessage;
    [SerializeField] PlayerStats playerStats;

    // Enables the Game Over UI when the player dies
    void OnEnable()
    {
        if (playerStats != null)
            playerStats.onPlayerDeath += HandlePlayerDeath;
    }

    // Disables the Game Over UI when the player dies
    void OnDisable()
    {
        if (playerStats != null)
            playerStats.onPlayerDeath -= HandlePlayerDeath;
    }

    void HandlePlayerDeath()
    {
        ShowEndScreen("You Died.", 0);
    }

    public void ShowEndScreen(string message, int winFlag)
    {
        if (EndScreenMessage != null)
            EndScreenMessage.text = message;

        // Player won the game, change the text color to green
        if (winFlag == 1)
        {
            EndScreenMessage.color = Color.darkGreen;
        }

        gameOverPanel.SetActive(true);

        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
