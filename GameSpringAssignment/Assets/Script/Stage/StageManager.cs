using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance { get; private set; }

    private IStageState currentState;
    private List<IStageState> stages;
    private ITimer stageTimer;
    private IScoreManager scoreManager;

    private StageEndUI stageEndUI;
    private bool isStageComplete = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        stages = new List<IStageState>
    {
        new Stage1State(),
        new Stage2State(),
        new Stage3State()
    };

        stageTimer = new StageTimer();
        scoreManager = new ScoreManager();

        stageEndUI = FindObjectOfType<StageEndUI>();

        TransitionToState(stages[0]);
    }

    void OnLevelWasLoaded(int level)
    {
        stageEndUI = FindObjectOfType<StageEndUI>();
    }

    void Update()
    {
        if (stageTimer != null)
        {
            stageTimer.UpdateTimer();
        }
    }

    public void TransitionToState(IStageState newState)
    {
        if (newState == null)
        {
            Debug.LogError("New state is null!");
            return;
        }

        currentState?.ExitState(this);
        currentState = newState;
        currentState.EnterState(this);
        ResetAndStartTimerForStage(currentState);

        isStageComplete = false;

        if (newState == stages[0])
        {
            scoreManager.ResetScore();
        }
    }

    public void OnStageComplete()
    {
        if (isStageComplete) return;

        isStageComplete = true;

        int nextStageIndex = stages.IndexOf(currentState) + 1;

        if (nextStageIndex < stages.Count)
        {
            int timeBonus = Mathf.FloorToInt(stageTimer.GetCurrentTime()) * 100;
            scoreManager.AddScore(timeBonus);

            if (stageEndUI != null)
            {
                stageEndUI.ShowStageEndUI(false);
            }
        }
        else
        {
            Debug.Log("All stages completed! Showing final end page.");
            if (stageEndUI != null)
            {
                int timeBonus = Mathf.FloorToInt(stageTimer.GetCurrentTime()) * 100;
                scoreManager.AddScore(timeBonus);

                stageEndUI.ShowStageEndUI(true);
            }
        }
    }

    public void OnNextStage()
    {
        int nextStageIndex = stages.IndexOf(currentState) + 1;

        if (nextStageIndex < stages.Count)
        {
            TransitionToState(stages[nextStageIndex]);
            LoadNextScene(nextStageIndex);
        }
        else
        {
            Debug.Log("All stages completed! Game Over.");
        }
    }

    public void ReplayGame()
    {
        scoreManager.ResetScore();
        TransitionToState(stages[0]);
        LoadNextScene(1);
    }

    private void LoadNextScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    private void ResetAndStartTimerForStage(IStageState stageState)
    {
        float stageTimeLimit = stageState.GetTimeLimit();
        stageTimer.ResetTimer();
        stageTimer.StartTimer(stageTimeLimit);
    }

    public void GameOver()
    {
        Debug.Log("Game Over! Showing final end page.");

        stageTimer.StopTimer();

        if (stageEndUI != null)
        {
            stageEndUI.ShowStageEndUI(true);
        }
    }

    public int GetCardCountForStage()
    {
        return currentState?.GetCardCount() ?? 0;
    }

    public int GetRowLengthForStage()
    {
        return currentState?.GetRowLength() ?? 0;
    }

    public ITimer GetTimer()
    {
        return stageTimer;
    }

    public IScoreManager GetScoreManager()
    {
        return scoreManager;
    }

    public void AddScore(int score)
    {
        scoreManager.AddScore(score);
    }
}
