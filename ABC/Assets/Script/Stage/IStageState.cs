public interface IStageState
{
    void EnterState(StageManager stageManager);
    void ExitState(StageManager stageManager);
    int GetCardCount();
    int GetRowLength();
    float GetTimeLimit();
}
