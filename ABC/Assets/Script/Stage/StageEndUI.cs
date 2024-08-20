using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StageEndUI : MonoBehaviour
{
    public GameObject endPage; // �Ϲ� �������� ���� ������
    public GameObject finalEndPage; // ������ �������� ���� ������
    public Button nextStageButton; // ���� ���������� �Ѿ�� ��ư
    public Button replayButton_0; // ������ �ٽ� �����ϴ� ��ư
    public Button replayButton_1; // ������ �ٽ� �����ϴ� ��ư
    public TextMeshProUGUI scoreText; // ���� ������ ǥ���� �ؽ�Ʈ
    public TextMeshProUGUI highScoreText; // �ְ� ������ ǥ���� �ؽ�Ʈ

    void Start()
    {
        // �ʱ⿡�� ��� ���� �������� ��Ȱ��ȭ
        endPage.SetActive(false);
        finalEndPage.SetActive(false);
    }

    public void ShowStageEndUI(bool isFinalStage)
    {
        // Ÿ�̸� ����
        StageManager.Instance.GetTimer().StopTimer();

        // ���� ������ �ְ� ���� ǥ��
        int currentScore = StageManager.Instance.GetScoreManager().GetScore();
        int highScore = StageManager.Instance.GetScoreManager().GetHighScore();

        scoreText.text = "Score: " + currentScore;
        highScoreText.text = "High Score: " + highScore;

        if (isFinalStage)
        {
            // ������ ���������� ���, ������ ���� �������� Ȱ��ȭ
            finalEndPage.SetActive(true);
            replayButton_0.onClick.AddListener(OnReplayButtonClicked);
        }
        else
        {
            // �Ϲ� ���������� ���, �Ϲ� ���� �������� Ȱ��ȭ
            endPage.SetActive(true);
            nextStageButton.onClick.AddListener(OnNextStageButtonClicked);
            replayButton_1.onClick.AddListener(OnReplayButtonClicked);
        }
    }

    private void OnNextStageButtonClicked()
    {
        // �Ϲ� ���� �������� �ݰ� ���� ���������� �̵�
        endPage.SetActive(false);
        StageManager.Instance.OnNextStage();
    }

    private void OnReplayButtonClicked()
    {
        // ������ ���� �������� �ݰ� ������ ó������ �ٽ� ����
        finalEndPage.SetActive(false);
        StageManager.Instance.ReplayGame();
    }
}
