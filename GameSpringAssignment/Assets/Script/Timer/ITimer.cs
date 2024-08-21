public interface ITimer
{
    void StartTimer(float duration);
    void UpdateTimer();
    void ResetTimer();
    void StopTimer();
    float GetCurrentTime();
}