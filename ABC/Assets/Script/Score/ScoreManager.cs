using UnityEngine;

public class ScoreManager : IScoreManager
{
    private int currentScore;
    private int highScore;

    private const string HighScoreKey = "HighScore"; // PlayerPrefs에서 사용할 키

    public ScoreManager()
    {
        // 게임 시작 시 최고점수를 로드
        highScore = PlayerPrefs.GetInt(HighScoreKey, 0);
    }

    public void AddScore(int score)
    {
        currentScore += score;
        // 최고점수를 갱신했는지 확인
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
        // 최고점수를 PlayerPrefs에 저장
        PlayerPrefs.SetInt(HighScoreKey, highScore);
        PlayerPrefs.Save();
    }
}
