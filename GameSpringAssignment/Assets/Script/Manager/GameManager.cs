using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public struct CardData
{
    public Suit suit;
    public int rank;
}

public class GameManager : MonoBehaviour
{
    public GameObject cardPrefab;
    public AudioSource audioSource; // AudioSource 컴포넌트
    public AudioClip cardFlipSound; // 카드 뒤집기 사운드
    public AudioClip timerSound;
    public AudioClip correctSound;

    private List<Card> cardList = new List<Card>();
    private List<Card> flippedCards = new List<Card>();
    private int consecutiveMatches = 0; // 연속으로 맞춘 횟수를 추적

    private StageManager stageManager;
    private ISoundManager soundManager;

    void Start()
    {
        stageManager = StageManager.Instance;

        if (stageManager == null)
        {
            Debug.LogError("StageManager instance not found!");
            return;
        }

        // SoundManager 초기화
        soundManager = new SoundManager(audioSource, cardFlipSound, timerSound, correctSound);

        Card.OnCardClicked += HandleCardClicked;
        MouseInputManager.OnMouseClicked += HandleMouseClick;

        SetupStage();
    }

    public void ResetGame()
    {
        // 카드 리스트 초기화
        cardList.Clear();
        flippedCards.Clear();

        // 필요한 경우 추가적인 상태 초기화
        consecutiveMatches = 0;
        soundManager?.StopTimerSound();
    }

    private void SetupStage()
    {
        soundManager.PlayTimerSound();

        int cardCount = stageManager.GetCardCountForStage();
        int rowLength = stageManager.GetRowLengthForStage();
        float cardSpacing = 2.5f;

        int cardTypes = cardCount / 2;

        List<CardData> cardDatas = GenerateCardDatas(cardTypes);

        float gridWidth = (rowLength - 1) * cardSpacing;
        int rowCount = Mathf.CeilToInt((float)cardCount / rowLength); // 행 수 계산
        float gridHeight = (rowCount - 1) * cardSpacing * 1f; // 카드 간격 조정 (1.5는 세로 간격의 배수)

        for (int i = 0; i < cardCount; i++)
        {
            int row = i / rowLength;
            int col = i % rowLength;

            float x = -gridWidth / 2 + col * cardSpacing;
            float y = gridHeight / 2 - row * cardSpacing * 1.5f; // 중앙 정렬을 위해 계산

            Vector3 spawnPosition = new Vector3(x, y, 0);

            Card card = CardFactory.CreateCard(cardDatas[i].suit, cardDatas[i].rank, cardPrefab);
            card.transform.position = spawnPosition;
            cardList.Add(card);
        }
    }


    private List<CardData> GenerateCardDatas(int cardTypes)
    {
        List<CardData> cardDatas = new List<CardData>();
        List<int> usedCards = new List<int>();

        for (int i = 0; i < cardTypes; i++)
        {
            int cardIndex;
            do
            {
                cardIndex = Random.Range(1, 14);
            } while (usedCards.Contains(cardIndex));

            usedCards.Add(cardIndex);

            Suit suit = (Suit)Random.Range(0, 4);

            cardDatas.Add(new CardData { suit = suit, rank = cardIndex });
            cardDatas.Add(new CardData { suit = suit, rank = cardIndex });
        }

        cardDatas = cardDatas.OrderBy(x => Random.value).ToList();

        return cardDatas;
    }

    private void HandleCardClicked(Card clickedCard)
    {
        if (this == null) return;

        if (flippedCards.Contains(clickedCard) || flippedCards.Count > 1)
            return;

        if (!clickedCard.IsFaceUp)
        {
            // 카드 뒤집기 소리 재생
            soundManager.PlayCardFlipSound();

            clickedCard.FlipCard();
            flippedCards.Add(clickedCard);
        }
        else
        {
            return;
        }

        if (flippedCards.Count == 2)
        {
            if (this != null)
            {
                StartCoroutine(CheckForMatch());
            }
        }
    }

    private IEnumerator CheckForMatch()
    {
        yield return new WaitForSeconds(1f);

        if (flippedCards[0].Rank == flippedCards[1].Rank && flippedCards[0].Suit == flippedCards[1].Suit)
        {
            Debug.Log("Match!");

            soundManager.PlayCorrectSound();

            Vector3 targetPosition = new Vector3(-10f, 4.5f, 1f); // 카드들이 모일 위치
            float moveDuration = 0.2f; // 카드가 이동하는 데 걸리는 시간

            foreach (var card in flippedCards)
            {
                cardList.Remove(card);
                StartCoroutine(MoveCardToPosition(card, targetPosition, moveDuration));
            }

            flippedCards.Clear();

            consecutiveMatches++; // 연속 맞춤 횟수 증가
            int scoreToAdd = 100 * (int)Mathf.Pow(3, consecutiveMatches - 1); // 연속 횟수에 따라 3배수 증가
            StageManager.Instance.AddScore(scoreToAdd);

            if (AllCardsMatched())
            {
                soundManager.PlayCorrectSound();
                soundManager.StopTimerSound();
                StageManager.Instance.OnStageComplete();
            }
        }
        else
        {
            Debug.Log("Mismatch!");
            flippedCards[0].FlipCard();
            flippedCards[1].FlipCard();
            flippedCards.Clear();

            consecutiveMatches = 0; // 맞추지 못했을 경우 연속 맞춤 횟수 초기화
        }
    }

    private IEnumerator MoveCardToPosition(Card card, Vector3 targetPosition, float duration)
    {
        Vector3 startPosition = card.transform.position;
        Quaternion startRotation = card.transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0, 0, Random.Range(0, 360f)); // Y축 회전을 랜덤으로 설정

        float time = 0f;
        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;

            // 카드 위치와 회전을 동시에 보간
            card.transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            card.transform.rotation = Quaternion.Lerp(startRotation, targetRotation, t);

            yield return null;
        }

        // 최종적으로 타겟 위치와 회전 상태에 정확히 맞추기
        card.transform.position = targetPosition;
        card.transform.rotation = targetRotation;
    }

    private bool AllCardsMatched()
    {
        return cardList.Count == 0;
    }

    private void HandleMouseClick(Vector3 position)
    {
        if (this == null) return;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
    }

    private void OnDestroy()
    {
        Card.OnCardClicked -= HandleCardClicked;
        MouseInputManager.OnMouseClicked -= HandleMouseClick;
    }
}

