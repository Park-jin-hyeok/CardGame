using System.Collections;
using UnityEngine;

public class Stage3State : IStageState
{
    public void EnterState(StageManager stageManager)
    {
        Debug.Log("Entering Stage 3");
    }

    public void ExitState(StageManager stageManager)
    {
        Debug.Log("Exiting Stage 3");
    }

    public int GetCardCount() { return 18; }
    public int GetRowLength() { return 6; }
    public float GetTimeLimit() { return 60f; }
}

