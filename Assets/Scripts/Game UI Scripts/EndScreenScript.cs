using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject EndScreenPanel;
    [SerializeField] TMP_Text EndScreenMessage;

    public void ShowEndScreen(string message)
    {
        EndScreenMessage.text = message;
        EndScreenPanel.SetActive(true);

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
