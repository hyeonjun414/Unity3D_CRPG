using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardSpace
{
    Hands,
    Deck,
    Graveyard,
    Field,
}

public class CardManager : Singleton<CardManager>
{
    private int Null = -1;

    [Header("UI")]
    public CardHolder holder;
    public DeckUI deckUI;
    public GraveyardUI graveyardUI;

    [Header("Card")]
    public List<CardData> deck;
    public List<CardData> hands;
    public List<CardData> graveyard;
    public int maxHandCount = 5;
    public int maxDeckCount = 20;

    [Header("GuideLine")]
    public int pointCount;
    public LineRenderer guideLine;

    [Header("Bezier Pos")]
    public Transform t1;
    public Transform t2;
    public Transform t3;

    private Player player;
    private bool isDraw;
    private void Awake()
    {
        if(_instance == null)
            _instance = this;
    }
    private void Start()
    {
        // UI 조작을 위한 연결
        deckUI = UIManager.Instance.deckUI;
        holder = UIManager.Instance.cardHolder;
        graveyardUI = UIManager.Instance.graveyardUI;

        holder.handList = hands; // 카드 홀더에 매니저의 핸드 카드 리스트를 연결

        // 가이드라인을 위한 베지어 포인트를 대입
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        if(!isDraw)
        {
            holder.InputSelectCard();
        }

        // 만약 선택된 홀더에서 선택된 카드가 있다면
        if(holder.selectedCardIndex != Null)
        {
            //  가이드라인을 그린다.
            DrawGuideLine();
            //  카드 선택 상태에서 마우스 클릭을 처리한다.
            MouseClickDetection();
        }
    }

    // 필드에서 카드를 획득할때 사용된다.
    public void AddCard(CardData data)
    {
        // 덱의 카드 개수가 최대 개수이면 습득을 취소한다.
        if (deck.Count >= maxDeckCount) return;

        MoveCard(CardSpace.Field, CardSpace.Deck, data);
    }
    public void MoveCard(CardSpace from, CardSpace to, CardData data)
    {
        switch(from)
        {
            case CardSpace.Hands:
                hands.Remove(data);
                holder.UpdateUI();
                break;
            case CardSpace.Deck:
                deck.Remove(data);
                deckUI.UpdateUI();
                break;
            case CardSpace.Graveyard:
                graveyard.Remove(data);
                graveyardUI.UpdateUI();
                break;
        }

        switch (to)
        {
            case CardSpace.Hands:
                hands.Add(data);
                holder.UpdateUI();
                break;
            case CardSpace.Deck:
                deck.Add(data);
                deckUI.UpdateUI();
                break;
            case CardSpace.Graveyard:
                graveyard.Add(data);
                graveyardUI.UpdateUI();
                break;
        }
    }
    public void ActiveReroll()
    {
        StartCoroutine(RerollRoutine());
    }
    IEnumerator RerollRoutine()
    {
        yield return StartCoroutine(FromAtoB(CardSpace.Hands, CardSpace.Graveyard));
        yield return StartCoroutine(DrawRoutine());
    }
    public IEnumerator DrawRoutine()
    {
        isDraw = true;
        while(hands.Count < 5)
        {
            
            if(deck.Count == 0)
            {
                if(graveyard.Count == 0) 
                    break;

                yield return StartCoroutine(FromAtoB(CardSpace.Graveyard, CardSpace.Deck));
            }
            MoveCard(CardSpace.Deck, CardSpace.Hands, deck[0]);
            holder.UpdateUI();
            yield return new WaitForSeconds(0.2f);
        }
        isDraw = false;
    }
    IEnumerator FromAtoB(CardSpace a, CardSpace b)
    {
        List<CardData> aList = null;
        switch (a)
        {
            case CardSpace.Hands:
                aList = hands;
                break;
            case CardSpace.Deck:
                aList = deck;
                break;
            case CardSpace.Graveyard:
                aList = graveyard;
                break;
        }
        int aCount = aList.Count;

        ShuffleCard(a);

        for(int i = 0; i < aCount; i++)
        {
            MoveCard(a, b, aList[0]);
            yield return new WaitForSeconds(0.1f);
        }
    }
    public void ShuffleCard(CardSpace space)
    {
        switch(space)
        {
            case CardSpace.Graveyard: 
                for(int i = 0; i < graveyard.Count; i++)
                {
                    int rand = Random.Range(0, graveyard.Count);
                    CardData temp = graveyard[i];
                    graveyard[i] = graveyard[rand];
                    graveyard[rand] = temp;
                }
                break;
            case CardSpace.Deck:
                for (int i = 0; i < deck.Count; i++)
                {
                    int rand = Random.Range(0, deck.Count);
                    CardData temp = deck[i];
                    deck[i] = deck[rand];
                    deck[rand] = temp;
                }
                break;
        }
    }
    // 마우스 클릭 처리
    private void MouseClickDetection()
    {
        // 우클릭의 경우
        if (Input.GetMouseButtonDown(1))
        {
            // 선택된 카드를 초기화하고 가이드라인을 비활성화 한다.
            holder.selectedCardIndex = Null;
            guideLine.enabled = false;
        }
    }
    // 가이드라인(베지어 곡선 활용)을 그리는 함수
    public void DrawGuideLine()
    {
        // 레이캐스트를 하고 그라운드에 맞았다면 라인을 그리고 아니면 라인을 비활성화한다.
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 200, LayerMask.GetMask("Ground")))
        {
            // 베지어 포인트를 플레이어 - 충돌점과 플레이어의 사이 - 충돌점으로 대입한다.
            guideLine.enabled = true;
            t1.position = holder.cards[holder.selectedCardIndex].transform.position;
            t1.Translate(Vector3.back * 10);
            t3.position = raycastHit.point;
            t2.position = (t1.position + t3.position) * 0.5f + Vector3.up * 10;
            
            // 위치값을 설정한 값만큼 만든다.
            Vector3[] points = new Vector3[pointCount];

            // 베지어 위치를 구하는 함수를 이용해 각각의 포인트의 위치를 구해준다.
            for (int i = 0; i < points.Length; i++)
            {
                points[i] = GetBezierPos(t1.position, t2.position, t3.position, (float)i / (pointCount - 1));
            }

            // 라인 렌더러에 할당.
            guideLine.positionCount = pointCount;
            guideLine.SetPositions(points);
        }
        else
        {
            guideLine.enabled = false;
        }
    }
    // 베지어 함수
    private Vector3 GetBezierPos(Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        Vector3 q1 = Vector3.Lerp(p1, p2, t);
        Vector3 q2 = Vector3.Lerp(p2, p3, t);

        return Vector3.Lerp(q1, q2, t);
    }
}
