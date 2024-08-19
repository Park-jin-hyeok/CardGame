public class ScoreManager : IScoreManager
{
    private int currentScore;

    public void AddScore(int score)
    {
        currentScore += score;
    }

    public void ResetScore()
    {
        currentScore = 0;
    }

    public int GetScore()
    {
        return currentScore;
    }
}
