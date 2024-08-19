using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private Text TimerText;
    private float m_TotalTime = 0;

    //  매 프레임마다 실행되는 함수
    private void Update()
    {
        m_TotalTime += Time.deltaTime;

        //TimerText.text = "Time\n" + Time.deltaTime;
        TimerText.text = $"Time\n{m_TotalTime:F4}"; //  dynamic string, 보간된 문자열

        //  이스케이프 문자
        //  https://docs.microsoft.com/ko-kr/cpp/cpp/string-and-character-literals-cpp?view=msvc-160
        //  \t -> 한 번 탭 누른 효과
        //  \n -> 한 줄 내린다
    }

    public void StopTimer()
    {
        TimerText.text = $"Win!\n{m_TotalTime}";
    }

}
