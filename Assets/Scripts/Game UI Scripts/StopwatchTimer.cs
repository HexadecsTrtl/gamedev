using TMPro;
using UnityEngine;

public class StopwatchTimer : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TMP_Text timerText;

    float stopwatch = 0f;
    bool isRunning = true;

    // Update is called once per frame
    void Update()
    {
        if (!isRunning) return;
        stopwatch += Time.deltaTime;

        if (timerText != null)
        {
            timerText.text = GetFormattedTime();
        }
    }

    public string GetFormattedTime()
    {
        int min = Mathf.FloorToInt(stopwatch / 60);
        int sec = Mathf.FloorToInt(stopwatch % 60);
        int ms = Mathf.FloorToInt((stopwatch % 1) * 100);
        return string.Format("{0:00}:{1:00}:{2:00}", min, sec, ms);
    }

    public void StopTimer()
    {
        isRunning = false;
    }
}
