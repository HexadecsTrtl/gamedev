using UnityEngine;

public class LevelWinScript : MonoBehaviour
{
    [SerializeField] GameOverUI endScreen;
    [SerializeField] string winMessage = "You Win!";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            endScreen.ShowEndScreen(winMessage, 1);
        }
    }
}
