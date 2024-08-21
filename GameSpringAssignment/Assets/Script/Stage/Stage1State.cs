using UnityEngine;

public class Stage1State : IStageState
{
    public void EnterState(StageManager stageManager)
    {
        Debug.Log("Entering Stage 1");
    }

    public void ExitState(StageManager stageManager)
    {
        Debug.Log("Exiting Stage 1");
    }

    public int GetCardCount() { return 4; }
    public int GetRowLength() { return 2; }
    public float GetTimeLimit() { return 10f; }
}