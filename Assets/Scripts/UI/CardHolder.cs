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
    [SerializeField] private List<CardUnit> cards;  // 카드 리스트

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
        CardUnit[] cardArr = GetComponentsInChildren<CardUnit>();
        foreach(CardUnit card in cardArr)
        {
            cards.Add(card);
            if (card.cardData != null)
                card.UpdateUI();

        }
    }
    public void SetControllable(bool controllable) => isControllable = controllable;

    // Update is called once per frame
    void Update()
    {
        InputSelectCard();

        if(selectedCardIndex != Null)
        {
            Time.timeScale = 0.2f;
        }
        else
        {
            Time.timeScale = 1f;
        }

        // 마우스 클릭 감지
        if (isControllable) MouseClickDetection();

        ArrangeCards();
    }
    private void InputSelectCard()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1) && cards.Count >= 1)
        {
            selectedCardIndex = 0;
            bezierCurveDrawer.gameObject.SetActive(true);
            bezierCurveDrawer.points[0].transform.position = cards[selectedCardIndex].transform.position;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && cards.Count >= 2)
        {
            selectedCardIndex = 1;
            bezierCurveDrawer.gameObject.SetActive(true);
            bezierCurveDrawer.points[0].transform.position = cards[selectedCardIndex].transform.position;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && cards.Count >= 3)
        {
            selectedCardIndex = 2;
            bezierCurveDrawer.gameObject.SetActive(true);
            bezierCurveDrawer.points[0].transform.position = cards[selectedCardIndex].transform.position;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && cards.Count >= 4)
        {
            selectedCardIndex = 3;
            bezierCurveDrawer.gameObject.SetActive(true);
            bezierCurveDrawer.points[0].transform.position = cards[selectedCardIndex].transform.position;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5) && cards.Count >= 5)
        {
            selectedCardIndex = 4;
            bezierCurveDrawer.gameObject.SetActive(true);
            bezierCurveDrawer.points[0].transform.position = cards[selectedCardIndex].transform.position;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6) && cards.Count >= 6)
        {
            selectedCardIndex = 5;
            bezierCurveDrawer.gameObject.SetActive(true);
            bezierCurveDrawer.points[0].transform.position = cards[selectedCardIndex].transform.position;
        }
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
            if (i == selectedCardIndex)
            {
                targetPos = new Vector3(x, mouseOverYSpacing, zInterval);
                targetRot = Quaternion.identity;
                targetScl = Vector3.one * mouseOverScale;
            }
            else
            {
                targetPos = new Vector3(x, transform.position.y, i * -zInterval);
                targetRot = Quaternion.identity;
                targetScl = Vector3.one;

                // 마우스 오버 카드가 있을경우,
                if (selectedCardIndex != Null)
                    // 양옆으로 간격만큼 벌려줌
                    targetPos.x += (i < mouseOverCardIndex ? -1 : 1) * mouseOverInterval;
            }


            card.localPosition = Vector3.Lerp(card.localPosition, targetPos, lerpAmount);
            card.localRotation = Quaternion.Lerp(card.localRotation, targetRot, lerpAmount);
            card.localScale = Vector3.Lerp(card.localScale, targetScl, lerpAmount);
            card.SetSiblingIndex(i);
        }

        // 마우스 오버 카드가 있을경우,
        if (selectedCardIndex != Null)
            // 마우스 오버 카드를 맨 앞에 배치
            cards[selectedCardIndex].transform.SetAsLastSibling();

    }

    private void MouseClickDetection()
    {
        if(Input.GetMouseButtonDown(0) && selectedCardIndex != Null)
        {
            GameObject vfx = cards[selectedCardIndex].cardData.vfx;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, 200, LayerMask.GetMask("Ground")))
            {
                Instantiate(vfx, raycastHit.point + vfx.transform.position, vfx.transform.rotation);
            }
            selectedCardIndex = Null;
            bezierCurveDrawer.gameObject.SetActive(false);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            // 선택된 카드 초기화
            selectedCardIndex = Null;
            // 베지어 라인 비활성화
            bezierCurveDrawer.gameObject.SetActive(false);
        }
    }

}
