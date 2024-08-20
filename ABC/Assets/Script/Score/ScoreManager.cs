using UnityEngine;

public class ScoreManager : IScoreManager
{
    private int currentScore;
    private int highScore;

    private const string HighScoreKey = "HighScore"; // PlayerPrefs���� ����� Ű

    public ScoreManager()
    {
        // ���� ���� �� �ְ������� �ε�
        highScore = PlayerPrefs.GetInt(HighScoreKey, 0);
    }

    public void AddScore(int score)
    {
        currentScore += score;
        // �ְ������� �����ߴ��� Ȯ��
        if (currentScore > highScore)
        {
            highScore = currentScore;
            SaveHighScore();
        }
    }

    public void ResetScore()
    {
        currentScore = 0;
    }

    public int GetScore()
    {
        return currentScore;
    }

    public int GetHighScore()
    {
        return highScore;
    }

    private void SaveHighScore()
    {
        // �ְ������� PlayerPrefs�� ����
        PlayerPrefs.SetInt(HighScoreKey, highScore);
        PlayerPrefs.Save();
    }
}
