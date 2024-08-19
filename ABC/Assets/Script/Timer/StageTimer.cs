using UnityEngine;

public class StageTimer : ITimer
{
    private float currentTime;
    private bool isTimerRunning;

    public void StartTimer(float duration)
    {
        currentTime = duration;
        isTimerRunning = true;
    }

    public void UpdateTimer()
    {
        if (isTimerRunning && currentTime > 0)
        {
            currentTime -= Time.deltaTime;

            if (currentTime <= 0)
            {
                currentTime = 0;
                isTimerRunning = false;
                OnTimeExpired();
            }
        }
    }

    public void ResetTimer()
    {
        isTimerRunning = false;
        currentTime = 0;
    }

    public void StopTimer()
    {
        isTimerRunning = false;
    }

    public float GetCurrentTime()
    {
        return currentTime;
    }

    private void OnTimeExpired()
    {
        Debug.Log("Time's up! Game Over.");
        StageManager.Instance.GameOver();
    }
}
