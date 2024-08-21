using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Suit
{
    Heart,
    Diamond,
    Clover,
    Spade
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

    public void Initialize(Suit suit, int rank, Vector3 scale, int orderInLayer = 1) // scale 인자를 추가합니다.
    {
        this.Suit = suit;
        this.Rank = rank;
        this.IsFaceUp = false;

        spriteRenderer = GetComponent<SpriteRenderer>();

        cardBack = new GameObject("CardBack");
        var backRenderer = cardBack.AddComponent<SpriteRenderer>();
        backRenderer.sprite = Resources.Load<Sprite>("Cards/CardBack");
        backRenderer.sortingOrder = orderInLayer; // Order in Layer 설정
        cardBack.transform.SetParent(this.transform);
        cardBack.transform.localPosition = Vector3.zero;

        cardFront = new GameObject("CardFront");
        var frontRenderer = cardFront.AddComponent<SpriteRenderer>();
        string cardPath = $"Cards/{suit.ToString() + " "}{rank}";
        frontRenderer.sprite = Resources.Load<Sprite>(cardPath);
        frontRenderer.sortingOrder = orderInLayer; // Order in Layer 설정
        cardFront.transform.SetParent(this.transform);
        cardFront.transform.localPosition = Vector3.zero;

        cardFront.SetActive(false); 

        // 카드 크기 키우기 위함
        this.transform.localScale = new Vector3(-scale.x, scale.y, scale.z); // 좌우 반전

        // 선택되는 범위 넓히기
        boxCollider = GetComponent<BoxCollider2D>();
        if (boxCollider != null)
        {
            boxCollider.size = new Vector2(boxCollider.size.x / scale.x * 2.5f, boxCollider.size.y / scale.y * 2.5f);
        }
    }
    public void SetSortingOrder(int order)
    {
        SpriteRenderer frontRenderer = cardFront.GetComponent<SpriteRenderer>();
        SpriteRenderer backRenderer = cardBack.GetComponent<SpriteRenderer>();

        if (frontRenderer != null)
            frontRenderer.sortingOrder = order;

        if (backRenderer != null)
            backRenderer.sortingOrder = order;
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
        float halfDuration = duration / 2f;
        float time = 0f;
        Quaternion startRotation = transform.rotation;
        Quaternion midRotation = startRotation * Quaternion.Euler(0, 90f, 0);
        Quaternion endRotation = startRotation * Quaternion.Euler(0, 180f, 0);

        // Z축으로 살짝 앞으로 이동
        if (!IsFaceUp)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1.5f);
        }

        // 첫 절반 회전: 카드가 옆면을 향하게 함
        while (time < halfDuration)
        {
            time += Time.deltaTime;
            float t = time / halfDuration;
            transform.rotation = Quaternion.Lerp(startRotation, midRotation, t);
            yield return null;
        }

        // 중간 지점에서 앞면과 뒷면을 전환
        IsFaceUp = !IsFaceUp;
        cardBack.SetActive(!IsFaceUp);
        cardFront.SetActive(IsFaceUp);

        // 나머지 절반 회전: 카드가 완전히 뒤집히도록 함
        time = 0f;
        while (time < halfDuration)
        {
            time += Time.deltaTime;
            float t = time / halfDuration;
            transform.rotation = Quaternion.Lerp(midRotation, endRotation, t);
            yield return null;
        }

        // 회전 완료 후 회전 상태를 최종적으로 설정
        transform.rotation = startRotation * Quaternion.Euler(0, 180f, 0);

        // 카드가 이미 뒤집힌 상태라면 Z축 위치를 원래대로 복원
        if (!IsFaceUp)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1.5f);
        }

        isFlipping = false;
    }



    public void RemoveCard()
    {
        Destroy(this.gameObject);
    }
}
