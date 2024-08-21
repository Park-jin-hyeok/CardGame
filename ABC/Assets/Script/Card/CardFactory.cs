using UnityEngine;

public static class CardFactory
{
    public static Card CreateCard(Suit suit, int rank, GameObject cardPrefab)
    {
        GameObject cardObject = GameObject.Instantiate(cardPrefab);
        cardObject.AddComponent<BoxCollider2D>();

        Card card = cardObject.AddComponent<Card>();

        // 카드의 크기 설정
        Vector3 cardScale = new Vector3(1f, 1.4f, 1f);

        int orderInLayer = 1;

        card.Initialize(suit, rank, cardScale, orderInLayer);

        return card;
    }
}
