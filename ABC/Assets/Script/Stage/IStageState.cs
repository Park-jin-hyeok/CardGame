public interface IStageState
{
    void EnterState(StageManager stageManager);
    void ExitState(StageManager stageManager);
    int GetCardCount();
    int GetRowLength();
    float GetTimeLimit(); // 각 스테이지의 타이머 제한 시간을 반환
}
