using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardHolder : MonoBehaviour
{
    private const int Null = -1;

    [Header("연결")]
    [SerializeField] private GraphicRaycaster gr;          // 마우스 오버된 카드를 찾기 위한 레이케스터
    [SerializeField] private RectTransform rectTransform;           // 카드 홀더의 RectTransform
    [SerializeField] private BezierCurveDrawer bezierCurveDrawer;

    [Header("카드")]
    [SerializeField] private int selectedCardIndex; // 선택된 카드 인덱스
    [SerializeField] private int mouseOverCardIndex;// 마우스 오버된 카드의 인덱스
    [SerializeField] private List<Card> cards;  // 카드 리스트

    [Header("일반 카드 수치")]
    [SerializeField] private float lerpTime;        // lerp 정도
    [SerializeField] private float angularInterval; // 각도 간격
    [SerializeField] private float zInterval;       // z 간격
    [SerializeField] private float distance;

    [Header("마우스 오버 카드 수치")]
    [SerializeField] private float mouseOverInterval;   // 마우스 오버시 다른 카드들이 양옆으로 밀리는 간격
    [SerializeField] private float mouseOverScale;      // 마우스 오버된 카드의 스케일
    [SerializeField] private float mouseOverYSpacing;

    [Header("선택된 카드 수치")]
    [SerializeField] private float selectedScale;   // 선택된 카드의 스케일
    [SerializeField] private float selectedYSpacing;// 선택된 카드의 y 보정값

    private bool isControllable;   // 현재 컨트롤 가능 여부


    // Start is called before the first frame update
    void Start()
    {
        mouseOverCardIndex = Null;
        selectedCardIndex = Null;

        isControllable = true;
    }
    public void SetControllable(bool controllable) => isControllable = controllable;

    // Update is called once per frame
    void Update()
    {
        // 마우스 오버 감지
        MouseOverDetection();
        // 마우스 클릭 감지
        if (isControllable) MouseClickDetection();

        ArrangeCards();
    }

    private void ArrangeCards()
    {
        // 시작 각도
        float startAngle = angularInterval * 0.5f * (cards.Count - 1);
        float lerpAmount = lerpTime * Time.deltaTime;

        // 목표 수치들
        Vector3 targetPos;
        Quaternion targetRot;
        Vector3 targetScl;

        for(int i = 0; i < cards.Count; i++)
        {
            Transform card = cards[i].transform;

            float angle = startAngle + -angularInterval * i;
            float radian = angle * Mathf.Deg2Rad;

            float x = Mathf.Sin(-radian) * distance;
            float y = Mathf.Cos(radian) * distance - distance;

            // 만약 마우스 오버된 카드라면,
            if (i == mouseOverCardIndex)
            {
                targetPos = new Vector3(x, mouseOverYSpacing, zInterval);
                targetRot = Quaternion.identity;
                targetScl = Vector3.one * mouseOverScale;
            }
            else
            {
                targetPos = new Vector3(x, y, i * -zInterval);
                targetRot = Quaternion.Euler(0, 0, angle);
                targetScl = Vector3.one;

                // 마우스 오버 카드가 있을경우,
                if (mouseOverCardIndex != Null)
                    // 양옆으로 간격만큼 벌려줌
                    targetPos.x += (i < mouseOverCardIndex ? -1 : 1) * mouseOverInterval;
            }


            card.localPosition = Vector3.Lerp(card.localPosition, targetPos, lerpAmount);
            card.localRotation = Quaternion.Lerp(card.localRotation, targetRot, lerpAmount);
            card.localScale = Vector3.Lerp(card.localScale, targetScl, lerpAmount);
            card.SetSiblingIndex(i);
        }

        // 마우스 오버 카드가 있을경우,
        if (mouseOverCardIndex != Null)
            // 마우스 오버 카드를 맨 앞에 배치
            cards[mouseOverCardIndex].transform.SetAsLastSibling();

    }
    private void MouseOverDetection()
    {
        // Physics2DRaycaster를 이용하여 화면상에서 마우스 오버된 UI 오브젝트를 레이캐스트한다.
        var eventData = new PointerEventData(null) { position = Input.mousePosition };
        var results = new List<RaycastResult>();
        gr.Raycast(eventData, results);
        //print(results.Count);
        // 마우스 오버된 카드, 적을 초기화한다.
        mouseOverCardIndex = Null;

        // 마우스 오버된 오브젝트가 있을 경우,
        if (results.Count > 0)
        {
            // 태그 비교로 카드/에너미 리스트로 분리한다.
            var cardResults = results.Where(x => x.gameObject.CompareTag("Card")).ToList();

            print(cardResults.Count);

            // 선택된 카드가 없는 상태에서, 마우스 오버된 카드가 있을경우,
            if (selectedCardIndex == Null && cardResults.Count > 0)
            {
                // 카드중 가장 가까운 카드를 선택하여 (카드는 겹치므로)
                var result = cardResults.Aggregate((a, b) => a.distance > b.distance ? a : b);
                // 해당 카드의 인덱스를 얻는다.
                mouseOverCardIndex = cards.IndexOf(result.gameObject.GetComponent<Card>());
            }
        }
    }

    private void MouseClickDetection()
    {
        // 좌클릭시,
        if (Input.GetMouseButtonDown(0))
        {
            // 카드 선택 (마우스 오버된 카드가 있을 경우에만)
            if (mouseOverCardIndex != Null)
            {
                // 선택 카드 설정
                selectedCardIndex = mouseOverCardIndex;
                // 마우스 오버 카드 초기화
                mouseOverCardIndex = Null;
                // 공격 카드일 경우,
                bezierCurveDrawer.gameObject.SetActive(true);
                bezierCurveDrawer.points[0].transform.position = cards[selectedCardIndex].transform.position;
            }
        }
        // 우클릭시,
        else if (Input.GetMouseButtonDown(1))
        {
            // 선택된 카드 초기화
            selectedCardIndex = Null;
            // 베지어 라인 비활성화
            bezierCurveDrawer.gameObject.SetActive(false);
        }
    }

}
