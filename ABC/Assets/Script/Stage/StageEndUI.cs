using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StageEndUI : MonoBehaviour
{
    public GameObject endPage;
    public GameObject finalEndPage; 
    public Button nextStageButton; 
    public Button replayButton_0; 
    public Button replayButton_1; 
    public TextMeshProUGUI scoreText; 
    public TextMeshProUGUI highScoreText; 

    void Start()
    {
        endPage.SetActive(false);
        finalEndPage.SetActive(false);
    }

    public void ShowStageEndUI(bool isFinalStage)
    {
        StageManager.Instance.GetTimer().StopTimer();

        int currentScore = StageManager.Instance.GetScoreManager().GetScore();
        int highScore = StageManager.Instance.GetScoreManager().GetHighScore();

        scoreText.text =  currentScore.ToString();
        highScoreText.text = highScore.ToString();

        if (isFinalStage)
        {
            finalEndPage.SetActive(true);
            replayButton_0.onClick.AddListener(OnReplayButtonClicked);
        }
        else
        {
            endPage.SetActive(true);
            nextStageButton.onClick.AddListener(OnNextStageButtonClicked);
            replayButton_1.onClick.AddListener(OnReplayButtonClicked);
        }
    }

    private void OnNextStageButtonClicked()
    {
        endPage.SetActive(false);
        StageManager.Instance.OnNextStage();
    }

    private void OnReplayButtonClicked()
    {
        finalEndPage.SetActive(false);
        StageManager.Instance.ReplayGame();
    }
}
