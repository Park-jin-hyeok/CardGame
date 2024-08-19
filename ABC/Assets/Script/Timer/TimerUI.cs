using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour
{
    public TextMeshProUGUI timerText;

    private ITimer stageTimer;

    void Start()
    {
        stageTimer = StageManager.Instance.GetTimer();

        if (timerText == null)
        {
            Debug.LogError("TimerText is not assigned!");
        }
    }

    void Update()
    {
        if (stageTimer != null)
        {
            UpdateTimerText(stageTimer.GetCurrentTime());
        }
    }

    void UpdateTimerText(float currentTime)
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
