/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Suit
{
    Hearts,
    Diamonds,
    Clubs,
    Spades
}

public abstract class Card : MonoBehaviour
{
    public delegate void CardClickedEventHandler(Card clickedCard);
    public static event CardClickedEventHandler OnCardClicked;

    public static Dictionary<int, List<Vector2>> suitPositionDictionary = new Dictionary<int, List<Vector2>>
    {
        { 1, new List<Vector2> { new Vector2(0, 0) } },
        { 2, new List<Vector2> { new Vector2(0, 0.5f), new Vector2(0, -0.5f) } },
        { 3, new List<Vector2> { new Vector2(0, 0.7f), new Vector2(0, 0), new Vector2(0, -0.7f) } },
        { 4, new List<Vector2> { new Vector2(-0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(-0.5f, -0.5f), new Vector2(0.5f, -0.5f) } },
        { 5, new List<Vector2> { new Vector2(-0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0, 0), new Vector2(-0.5f, -0.5f), new Vector2(0.5f, -0.5f) } },
        { 6, new List<Vector2> { new Vector2(-0.5f, 0.7f), new Vector2(0.5f, 0.7f), new Vector2(-0.5f, 0), new Vector2(0.5f, 0), new Vector2(-0.5f, -0.7f), new Vector2(0.5f, -0.7f) } },
        { 7, new List<Vector2> { new Vector2(-0.5f, 0.8f), new Vector2(0, 0.8f), new Vector2(0.5f, 0.8f), new Vector2(-0.5f, 0), new Vector2(0.5f, 0), new Vector2(-0.5f, -0.8f), new Vector2(0.5f, -0.8f) } },
        { 8, new List<Vector2> { new Vector2(-0.5f, 0.8f), new Vector2(0.5f, 0.8f), new Vector2(-0.5f, 0.4f), new Vector2(0.5f, 0.4f), new Vector2(-0.5f, -0.4f), new Vector2(0.5f, -0.4f), new Vector2(-0.5f, -0.8f), new Vector2(0.5f, -0.8f) } },
        { 9, new List<Vector2> { new Vector2(-0.5f, 0.8f), new Vector2(0, 0.8f), new Vector2(0.5f, 0.8f), new Vector2(-0.5f, 0.4f), new Vector2(0, 0.4f), new Vector2(0.5f, 0.4f), new Vector2(-0.5f, -0.4f), new Vector2(0, -0.4f), new Vector2(0.5f, -0.4f) } },
        { 10, new List<Vector2> { new Vector2(-0.5f, 0.8f), new Vector2(0, 0.8f), new Vector2(0.5f, 0.8f), new Vector2(-0.5f, 0.4f), new Vector2(0, 0.4f), new Vector2(0.5f, 0.4f), new Vector2(-0.5f, -0.4f), new Vector2(0, -0.4f), new Vector2(0.5f, -0.4f), new Vector2(0, 0) } },
        { 11, new List<Vector2> { new Vector2(0, 0) } },
        { 12, new List<Vector2> { new Vector2(0, 0) } },
        { 13, new List<Vector2> { new Vector2(0, 0) } },
    };

    public Suit suit { get; protected set; }
    public int Rank { get; protected set; }
    public bool IsFaceUp { get; protected set; } // Add a flag for card state

    public abstract void Initialize(Suit suit, int rank);

    private bool isFlipping = false;

    private void OnMouseDown()
    {
        OnCardClicked?.Invoke(this);
    } 


    public virtual void FlipCard() // Make flip virtual for potential variations
    {
        IsFaceUp = !IsFaceUp;
        // Update card visuals based on IsFaceUp (show suit/rank or blank)
    } 

    public GameObject GetSuitPrefab(Suit suit)
    {
        switch (suit)
        {
            case global::Suit.Hearts:
                return Resources.Load<GameObject>("HeartPrefab");
            case global::Suit.Diamonds:
                return Resources.Load<GameObject>("DiamondPrefab");
            case global::Suit.Clubs:
                return Resources.Load<GameObject>("ClubPrefab");
            case global::Suit.Spades:
                return Resources.Load<GameObject>("SpadePrefab");
            default:
                Debug.LogError("Invalid suit");
                return null;
        }
    }
}

public class SingleCard : Card
{
    private SpriteRenderer spriteRenderer;
    private List<Vector2> suitPositions; // Dictionary to store suit positions

    public override void Initialize(Suit suit, int rank)
    {
        this.suit = suit;
        this.Rank = rank;
        this.IsFaceUp = false; // Start cards face down
        spriteRenderer = GetComponent<SpriteRenderer>();
        suitPositions = Card.suitPositionDictionary[rank]; // Initialize suit position dictionary

    }

    public override void FlipCard()
    {
        base.FlipCard(); // Call base class FlipCard logic

        if (IsFaceUp)
        {
            // Load and display card rank image (text or sprite depending on your design)
            // ... Load and display rank image based on Rank property
        }
        else
        {
            // Display the blank card asset
            spriteRenderer.sprite = Resources.Load<Sprite>("BlankCard"); // Assuming your blank card asset is named "BlankCard"
        }
    }
}



public class StandardCard : Card
{
    private float oscillationAmplitude = 0.05f; // Adjust amplitude as needed
    private float oscillationSpeed = 2f; // Adjust speed as needed

    private SpriteRenderer spriteRenderer;
    private List<Vector2> suitPositions; // Dictionary to store suit positions 

    private List<GameObject> suitSymbolObjList;

    public override void Initialize(Suit suit, int rank)
    {
        suitSymbolObjList = new List<GameObject>();
        this.suit = suit;
        this.Rank = rank;
        this.IsFaceUp = false; // Start cards face down
        spriteRenderer = GetComponent<SpriteRenderer>();
        suitPositions = Card.suitPositionDictionary[rank]; // get suit position from dictionary
    }

    public override void FlipCard()
    {
        base.FlipCard(); // Call base class FlipCard logic

        if (IsFaceUp)
        { 

            foreach (Vector2 suitPos in suitPositions)
            {
                
                
                // Instantiate the suit symbol at the specified position
                GameObject suitSymbol = Instantiate(GetSuitPrefab(suit));

                suitSymbol.transform.SetParent(this.gameObject.transform); 
                suitSymbol.transform.localPosition = suitPos / 2f; // Set position relative to the card  
                suitSymbol.transform.localScale = 0.1f * Vector3.one; // Ensure correct scaling if needed
                suitSymbol.tag = "SuitSymbol"; // Tag to identify suit symbols 
                // add suit symbol  
                suitSymbolObjList.Add(suitSymbol);
            }
        }
        else
        {
            ClearSuitSymbols();
            // Display the blank card asset
            // spriteRenderer.sprite = Resources.Load<Sprite>("BlankCard"); // Assuming your blank card asset is named "BlankCard"
        }
    }

    public void Update()
    {


        if (IsFaceUp)
        {
            for (int i = 0; i < suitPositions.Count; i++) {
                float offset = Mathf.Sin(Time.time * oscillationSpeed + i) * oscillationAmplitude;
                suitSymbolObjList[i].transform.localPosition = suitPositions[i]/2 + new Vector2(offset, 0);
            }
        }
    }

    private void ClearSuitSymbols()
    {
        // Optionally, remove previously instantiated suit symbols
        foreach (Transform child in transform)
        {
            if (child.CompareTag("SuitSymbol"))
            {
                Destroy(child.gameObject);
            }
        }

        suitSymbolObjList.Clear();
    }
}
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Suit
{
    하트,
    다이아,
    클로버,
    스페이드
}

public class Card : MonoBehaviour
{
    public delegate void CardClickedEventHandler(Card clickedCard);
    public static event CardClickedEventHandler OnCardClicked;

    public Suit Suit { get; private set; }
    public int Rank { get; private set; }
    public bool IsFaceUp { get; private set; }

    private SpriteRenderer spriteRenderer;
    private GameObject cardFront; // 앞면 이미지 오브젝트
    private GameObject cardBack;  // 뒷면 이미지 오브젝트
    private BoxCollider2D boxCollider; // BoxCollider2D 참조
    private bool isFlipping = false;

    public void Initialize(Suit suit, int rank, Vector3 scale) // scale 인자를 추가합니다.
    {
        this.Suit = suit;
        this.Rank = rank;
        this.IsFaceUp = false;

        spriteRenderer = GetComponent<SpriteRenderer>();

        // Load and set the back sprite
        cardBack = new GameObject("CardBack");
        var backRenderer = cardBack.AddComponent<SpriteRenderer>();
        backRenderer.sprite = Resources.Load<Sprite>("Cards/CardBack");
        cardBack.transform.SetParent(this.transform);
        cardBack.transform.localPosition = Vector3.zero;

        // Load and set the front sprite based on suit and rank
        cardFront = new GameObject("CardFront");
        var frontRenderer = cardFront.AddComponent<SpriteRenderer>();
        string cardPath = $"Cards/{suit.ToString() + " "}{rank}";
        frontRenderer.sprite = Resources.Load<Sprite>(cardPath);
        cardFront.transform.SetParent(this.transform);
        cardFront.transform.localPosition = Vector3.zero;

        cardFront.SetActive(false); // Initially, front is hidden

        // Set the scale of the card
        this.transform.localScale = scale;

        // Adjust the BoxCollider2D size to compensate for the scaling
        boxCollider = GetComponent<BoxCollider2D>();
        if (boxCollider != null)
        {
            boxCollider.size = new Vector2(boxCollider.size.x / scale.x, boxCollider.size.y / scale.y);
        }
    }

    private void OnMouseDown()
    {
        if (!isFlipping)
        {
            OnCardClicked?.Invoke(this);
        }
    }

    public void FlipCard()
    {
        StartCoroutine(FlipAnimation());
    }

    private IEnumerator FlipAnimation()
    {
        isFlipping = true;
        float duration = 0.5f; // Flip duration
        float time = 0f;
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = transform.rotation * Quaternion.Euler(0, 180f, 0);

        while (time < duration)
        {
            time += Time.deltaTime;
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, time / duration);
            yield return null;
        }

        IsFaceUp = !IsFaceUp;
        cardBack.SetActive(!IsFaceUp);
        cardFront.SetActive(IsFaceUp);

        transform.rotation = startRotation; // Reset rotation after flip
        isFlipping = false;
    }

    public void RemoveCard()
    {
        Destroy(this.gameObject);
    }
}