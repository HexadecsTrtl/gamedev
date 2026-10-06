using TMPro;
using UnityEngine;

public class CountdownTimer : MonoBehaviour
{
    [SerializeField] GameOverUI endScreen;
    [SerializeField] string timeUpMessage = "Time's Up!";

    [SerializeField] TMP_Text timerText;
    [SerializeField] float remainingTime = 90f; // Initial countdown time is 90s

    bool isTimeUp = false;

    // Update is called once per frame
    void Update()
    {
        remainingTime -= Time.deltaTime;

        if (remainingTime <= 0 && !isTimeUp)
        {
            remainingTime = 0f;
            isTimeUp = true;
            endScreen.ShowEndScreen(timeUpMessage, 0);
            timerText.color = Color.red;
        } else
        {
            int min = Mathf.FloorToInt(remainingTime / 60);
            int sec = Mathf.FloorToInt(remainingTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", min, sec);
        }
    }
}
