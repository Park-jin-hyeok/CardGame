public interface IScoreManager
{
    void AddScore(int score);
    void ResetScore();
    int GetScore();
    int GetHighScore(); // 최고 점수를 반환하는 메서드 추가
}
