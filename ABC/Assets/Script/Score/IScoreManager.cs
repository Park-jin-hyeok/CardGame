public interface IScoreManager
{
    void AddScore(int score);
    void ResetScore();
    int GetScore();
    int GetHighScore(); // �ְ� ������ ��ȯ�ϴ� �޼��� �߰�
}
