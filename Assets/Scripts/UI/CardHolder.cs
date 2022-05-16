using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardHolder : MonoBehaviour
{
    private const int Null = -1;

    [Header("카드")]
    [SerializeField] public int selectedCardIndex; // 선택된 카드 인덱스
    [SerializeField] public List<CardUnit> cards;  // 카드 리스트

    [Header("일반 카드 수치")]
    [SerializeField] private float lerpTime;        // lerp 정도
    [SerializeField] private float xInterval;

    [Header("선택된 카드 수치")]
    [SerializeField] private float selectedScale;   // 선택된 카드의 스케일
    [SerializeField] private float selectedYSpacing;// 선택된 카드의 y 보정값
    [SerializeField] private float selectInterval;   // 마우스 오버시 다른 카드들이 양옆으로 밀리는 간격


    public List<CardData> handList;
    void Start()
    {
        selectedCardIndex = Null;

        CardUnit[] cardArr = GetComponentsInChildren<CardUnit>();
        cards = cardArr.ToList();
        foreach(CardUnit card in cards)
        {
            card.gameObject.SetActive(false);
        }
        
        UpdateUI();
    }

    public void UpdateUI()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            if(i <handList.Count)
            {
                cards[i].AddCard(handList[i]);
            }
            else
            {
                cards[i].DeleteCard();
            }
        }
    }

    void Update()
    {
        if (GameManager.Instance.IsPause) return;

        ArrangeCards();

    }
    public void InputSelectCard()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1) && handList.Count >= 1)
        {
            selectedCardIndex = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && handList.Count >= 2)
        {
            selectedCardIndex = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && handList.Count >= 3)
        {
            selectedCardIndex = 2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && handList.Count >= 4)
        {
            selectedCardIndex = 3;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5) && handList.Count >= 5)
        {
            selectedCardIndex = 4;
        }
    }

    private void ArrangeCards()
    {
        float lerpAmount = lerpTime * Time.unscaledDeltaTime;

        // 목표 수치들
        Vector3 targetPos;
        Quaternion targetRot;
        Vector3 targetScl;

        for(int i = 0; i < cards.Count; i++)
        {
            if (!cards[i].gameObject.activeSelf) continue;
            Transform card = cards[i].transform;


            // 만약 마우스 오버된 카드라면,
            if (i == selectedCardIndex)
            {
                targetPos = new Vector3(i * xInterval, selectedYSpacing, 0);
                targetRot = Quaternion.identity;
                targetScl = Vector3.one * selectedScale;
            }
            else
            {
                targetPos = new Vector3(i * xInterval, 0, 0);
                targetRot = Quaternion.identity;
                targetScl = Vector3.one;

                // 마우스 오버 카드가 있을경우,
                if (selectedCardIndex != Null)
                    // 양옆으로 간격만큼 벌려줌
                    targetPos.x += (i < selectedCardIndex ? -1 : 1) * selectInterval;
            }


            card.localPosition = Vector3.Lerp(card.localPosition, targetPos, lerpAmount);
            card.localRotation = Quaternion.Lerp(card.localRotation, targetRot, lerpAmount);
            card.localScale = Vector3.Lerp(card.localScale, targetScl, lerpAmount);
            //card.SetSiblingIndex(i);
        }

    }



}
