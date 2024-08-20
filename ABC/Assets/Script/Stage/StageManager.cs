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
    private bool isStageComplete = false; // 스테이지 완료 상태를 확인하기 위한 플래그

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

        // 두 번째 씬에서도 StageEndUI를 다시 찾아 참조합니다.
        stageEndUI = FindObjectOfType<StageEndUI>();

        TransitionToState(stages[0]);
    }

    void OnLevelWasLoaded(int level)
    {
        // 씬이 로드될 때마다 StageEndUI를 다시 찾습니다.
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

        isStageComplete = false; // 새로운 스테이지 시작 시 스테이지 완료 상태를 초기화

        if (newState == stages[0])
        {
            scoreManager.ResetScore();
        }
    }

    public void OnStageComplete()
    {
        if (isStageComplete) return; // 스테이지 완료가 이미 처리된 경우 반복 호출 방지

        isStageComplete = true;

        int nextStageIndex = stages.IndexOf(currentState) + 1;

        if (nextStageIndex < stages.Count)
        {
            // 남은 시간에 따라 추가 점수 계산
            int timeBonus = Mathf.FloorToInt(stageTimer.GetCurrentTime()) * 100;
            scoreManager.AddScore(timeBonus);

            // 스테이지 완료 UI 표시 (마지막 스테이지가 아닌 경우)
            if (stageEndUI != null)
            {
                stageEndUI.ShowStageEndUI(false);
            }
        }
        else
        {
            Debug.Log("All stages completed! Showing final end page.");
            // 마지막 스테이지 완료 UI 표시
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
        // 게임을 처음부터 다시 시작
        scoreManager.ResetScore();
        TransitionToState(stages[0]);
        LoadNextScene(0);
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

        // 타이머 멈춤
        stageTimer.StopTimer();

        // 현재 점수를 표시하고 finalEndPage 활성화
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
