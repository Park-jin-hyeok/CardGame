using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Text m_ScoreText;
    private int m_Score = 0;

    public void AddScore(int score)
    {
        m_Score += score;
        m_ScoreText.text = $"Score\n{m_Score}";
    }

}
