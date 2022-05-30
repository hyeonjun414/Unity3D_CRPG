using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckInfoUI : MonoBehaviour
{
    public Text deckInfoTitle;
    public ScrollRect scrollView;
    public CardUI[] cardUI;

    List<CardData> cardList;



    public void UpdateUI()
    {
        for (int i = 0; i < cardUI.Length; i++)
        {
            if(i < cardList.Count)
            {
                cardUI[i].AddCard(cardList[i]);
            }
            else
            {
                cardUI[i].ResetCard();
            }
        }
    }

    public void OpenDeckInfo()
    {
        deckInfoTitle.text = "DECK";
        
        cardList = CardManager.Instance.deck;
        UpdateUI();
        scrollView.normalizedPosition = Vector3.one;
        gameObject.SetActive(true);
    }

    public void OpenGYInfo()
    {
        deckInfoTitle.text = "Graveyard";
        
        cardList = CardManager.Instance.graveyard;
        UpdateUI();
        scrollView.normalizedPosition = Vector3.one;
        gameObject.SetActive(true);
    }
    public void CloseUI()
    {
        gameObject.SetActive(false);
    }
}
