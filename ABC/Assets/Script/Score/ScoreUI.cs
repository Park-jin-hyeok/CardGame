using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // TextMeshProUGUI�� ���� ����

    private IScoreManager scoreManager;

    void Start()
    {
        // StageManager�� ScoreManager�� ����
        scoreManager = StageManager.Instance.GetScoreManager();

        if (scoreText == null)
        {
            Debug.LogError("ScoreText is not assigned!");
        }

        UpdateScoreText();
    }

    void Update()
    {
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + scoreManager.GetScore().ToString();
    }
}
