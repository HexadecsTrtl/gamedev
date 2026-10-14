using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TMP_Text remainingTargetsText;
    [SerializeField] EndScreenUI gameEndScreen;
    [SerializeField] StopwatchTimer stopwatchTimer;
    [SerializeField] ThrowingScript throwingScript;

    int totalStacks, remainingStacks;
    bool gameEnded = false;

    private void OnEnable()
    {
        CanStack.OnStackKnockedOver += HandleStackKnockedOver;
    }

    private void OnDisable()
    {
        CanStack.OnStackKnockedOver -= HandleStackKnockedOver;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        totalStacks = FindObjectsByType<CanStack>(FindObjectsInactive.Exclude).Length;
        remainingStacks = totalStacks;

        UpdateTargetCountText();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameEnded) return;

        if (throwingScript != null && throwingScript.GetTotalThrows() <= 0 && remainingStacks > 0)
        {
            TriggerGameOver();
        }
    }

    private void HandleStackKnockedOver()
    {
        if (gameEnded) return;

        remainingStacks--;
        UpdateTargetCountText();

        // If all stacks are knocked down, trigger win state
        if (remainingStacks <= 0)
        {
            TriggerWin();
        }
    }

    private void TriggerWin()
    {
        gameEnded = true;
        stopwatchTimer.StopTimer();
        gameEndScreen.ShowEndScreen("You Win!");
    }

    private void TriggerGameOver()
    {
        gameEnded = true;
        stopwatchTimer.StopTimer();
        gameEndScreen.ShowEndScreen("Game Over!");
    }

    private void UpdateTargetCountText()
    {
        remainingTargetsText.text = $"Targets: {Mathf.Max(remainingStacks, 0)}/{totalStacks}";
    }
}
