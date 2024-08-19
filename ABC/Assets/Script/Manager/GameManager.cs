/*
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject cardPrefab; // Reference to your card prefab 

    private List<Card> cardList = new List<Card>();
    private List<Card> flippedCards = new List<Card>();

    private StageManager stageManager; // StageManager에 대한 참조

    void Start()
    {
        // StageManager 인스턴스 가져오기
        stageManager = StageManager.Instance;

        if (stageManager == null)
        {
            Debug.LogError("StageManager instance not found!");
            return;
        }

        Card.OnCardClicked += HandleCardClicked;
        MouseInputManager.OnMouseClicked += HandleMouseClick;

        SetupStage();
    }

    private void SetupStage()
    {
        int cardCount = stageManager.GetCardCountForStage();
        int rowLength = stageManager.GetRowLengthForStage();
        float cardSpacing = 1.5f;

        int cardTypes = cardCount / 2;

        // Generate a list of unique card pairs
        List<CardData> cardDatas = GenerateCardDatas(cardTypes);

        // Calculate grid dimensions
        float gridWidth = (rowLength - 1) * cardSpacing;
        float gridHeight = (cardCount / rowLength - 1) * cardSpacing;

        // Create cards based on cardDatas
        for (int i = 0; i < cardCount; i++)
        {
            int row = i / rowLength;
            int col = i % rowLength;

            // Calculate centered position
            float x = -gridWidth / 2 + col * cardSpacing;
            float y = gridHeight / 2 - row * cardSpacing;
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
                cardIndex = Random.Range(1, 11); // Adjust range as needed
            } while (usedCards.Contains(cardIndex));

            usedCards.Add(cardIndex);

            // Generate random suit
            Suit suit = (Suit)Random.Range(0, 4);

            // Create two cards with the same suit and rank
            cardDatas.Add(new CardData { suit = suit, rank = cardIndex });
            cardDatas.Add(new CardData { suit = suit, rank = cardIndex });
        }

        // Shuffle the cardDatas
        cardDatas = cardDatas.OrderBy(x => Random.value).ToList();

        return cardDatas;
    }

    private void HandleCardClicked(Card clickedCard)
    {
        if (this == null) return; // GameManager가 유효한지 확인

        // Check if the card is already flipped
        if (flippedCards.Contains(clickedCard) || flippedCards.Count > 1)
            return;

        if (!clickedCard.IsFaceUp)
        {
            // Face down
            clickedCard.FlipCard();
            flippedCards.Add(clickedCard);
            Debug.Log($"Card Revealed FlipCardNum: {flippedCards.Count}");
        }
        else
        {
            return;
        }

        if (flippedCards.Count == 2)
        {
            // 코루틴 시작 전에 GameManager가 파괴되지 않았는지 확인
            if (this != null)
            {
                StartCoroutine(CheckForMatch());
            }
        }
    }

    private IEnumerator CheckForMatch()
    {
        yield return new WaitForSeconds(0.5f);

        if (flippedCards[0].Rank == flippedCards[1].Rank)
        {
            Debug.Log("Match!");
            flippedCards.Clear();

            if (AllCardsMatched())
            {
                stageManager.OnStageComplete(); // 스테이지 완료를 StageManager에 알림
            }
        }
        else
        {
            Debug.Log("Mismatch!");
            flippedCards[0].FlipCard();
            flippedCards[1].FlipCard();
            flippedCards.Clear();
        }
    }

    private bool AllCardsMatched()
    {
        foreach (var card in cardList)
        {
            if (!card.IsFaceUp)
            {
                return false;
            }
        }
        return true;
    }

    private void HandleMouseClick(Vector3 position)
    {
        if (this == null) return; // GameManager가 유효한지 확인
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
    }

    private void OnDestroy()
    {
        // 이벤트 핸들러 제거
        Card.OnCardClicked -= HandleCardClicked;
        MouseInputManager.OnMouseClicked -= HandleMouseClick;
    }
}

public struct CardData
{
    public Suit suit;
    public int rank;
}
*/

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

    private List<Card> cardList = new List<Card>();
    private List<Card> flippedCards = new List<Card>();

    private StageManager stageManager;

    void Start()
    {
        stageManager = StageManager.Instance;

        if (stageManager == null)
        {
            Debug.LogError("StageManager instance not found!");
            return;
        }

        Card.OnCardClicked += HandleCardClicked;
        MouseInputManager.OnMouseClicked += HandleMouseClick;

        SetupStage();
    }

    private void SetupStage()
    {
        int cardCount = stageManager.GetCardCountForStage();
        int rowLength = stageManager.GetRowLengthForStage();
        float cardSpacing = 1.5f;

        int cardTypes = cardCount / 2;

        List<CardData> cardDatas = GenerateCardDatas(cardTypes);

        float gridWidth = (rowLength - 1) * cardSpacing;
        float gridHeight = (cardCount / rowLength - 1) * cardSpacing;

        for (int i = 0; i < cardCount; i++)
        {
            int row = i / rowLength;
            int col = i % rowLength;

            float x = -gridWidth / 2 + col * cardSpacing;
            float y = gridHeight / 2 - row * cardSpacing;
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
            foreach (var card in flippedCards)
            {
                cardList.Remove(card); // 리스트에서 카드 제거
                card.RemoveCard();
            }

            flippedCards.Clear();

            // 짝이 맞을 때 점수 추가 (100점)
            StageManager.Instance.AddScore(100);

            if (AllCardsMatched())
            {
                StageManager.Instance.OnStageComplete();
            }
        }
        else
        {
            Debug.Log("Mismatch!");
            flippedCards[0].FlipCard();
            flippedCards[1].FlipCard();
            flippedCards.Clear();
        }
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
