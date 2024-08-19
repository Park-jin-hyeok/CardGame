using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // TextMeshProUGUI에 대한 참조

    private IScoreManager scoreManager;

    void Start()
    {
        // StageManager의 ScoreManager를 참조
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
