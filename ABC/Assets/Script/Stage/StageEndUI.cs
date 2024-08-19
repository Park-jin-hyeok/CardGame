using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StageEndUI : MonoBehaviour
{
    public GameObject endPage; // 일반 스테이지 엔드 페이지
    public GameObject finalEndPage; // 마지막 스테이지 엔드 페이지
    public Button nextStageButton; // 다음 스테이지로 넘어가는 버튼
    public Button replayButton_0; // 게임을 다시 시작하는 버튼
    public Button replayButton_1; // 게임을 다시 시작하는 버튼
    public TextMeshProUGUI scoreText; // 점수를 표시할 텍스트

    void Start()
    {
        // 초기에는 모든 엔드 페이지를 비활성화
        endPage.SetActive(false);
        finalEndPage.SetActive(false);
    }

    public void ShowStageEndUI(bool isFinalStage)
    {
        // 타이머 멈춤
        StageManager.Instance.GetTimer().StopTimer();

        // 현재 점수 표시
        scoreText.text = "Score: " + StageManager.Instance.GetScoreManager().GetScore();

        if (isFinalStage)
        {
            // 마지막 스테이지일 경우, 마지막 엔드 페이지를 활성화
            finalEndPage.SetActive(true);
            replayButton_0.onClick.AddListener(OnReplayButtonClicked);
        }
        else
        {
            // 일반 스테이지일 경우, 일반 엔드 페이지를 활성화
            endPage.SetActive(true);
            nextStageButton.onClick.AddListener(OnNextStageButtonClicked);
            replayButton_1.onClick.AddListener(OnReplayButtonClicked);
        }
    }

    private void OnNextStageButtonClicked()
    {
        // 일반 엔드 페이지를 닫고 다음 스테이지로 이동
        endPage.SetActive(false);
        StageManager.Instance.OnNextStage();
    }

    private void OnReplayButtonClicked()
    {
        // 마지막 엔드 페이지를 닫고 게임을 처음부터 다시 시작
        finalEndPage.SetActive(false);
        StageManager.Instance.ReplayGame();
    }
}
