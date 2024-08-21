using System.Collections;
using UnityEngine;

public class Stage2State : IStageState
{
    public void EnterState(StageManager stageManager)
    {
        Debug.Log("Entering Stage 2");
    }

    public void ExitState(StageManager stageManager)
    {
        Debug.Log("Exiting Stage 2");
    }

    public int GetCardCount() { return 8; }
    public int GetRowLength() { return 4; }
    public float GetTimeLimit() { return 30f; }
}
