/*
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public static class CardFactory
{
    public static Card CreateCard(Suit suit, int rank, GameObject cardPrefab)
    {
        GameObject cardObject = GameObject.Instantiate(cardPrefab);
        cardObject.AddComponent<BoxCollider2D>();


        Card card;

        // 2 ~ 10 standard cards with multiple suit images  
        if (rank > 1 || rank < 11)
        {
            card = cardObject.AddComponent<StandardCard>();
        }
        else if (rank == 1 || rank > 10)
        {
            // not implemented error
            card = cardObject.AddComponent<SingleCard>();
        }
        else
        {
            Debug.LogError("Non existing rank");
            return null;
        }

        if (card == null)
        {
            Debug.LogError("The prefab does not have a StandardCard component.");
            return null;
        }

        card.Initialize(suit, rank);

        // Generate card visuals based on suit, rank, and suitPositions dictionary
        // ... (Code for generating card visuals)

        return card;
    }
}
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CardFactory
{
    public static Card CreateCard(Suit suit, int rank, GameObject cardPrefab)
    {
        GameObject cardObject = GameObject.Instantiate(cardPrefab);
        cardObject.AddComponent<BoxCollider2D>();

        Card card = cardObject.AddComponent<Card>();

        // 원하는 크기로 스케일을 설정합니다 (예: 0.5배로 축소)
        Vector3 cardScale = new Vector3(0.2f, 0.2f, 1f);

        card.Initialize(suit, rank, cardScale);

        return card;
    }
}