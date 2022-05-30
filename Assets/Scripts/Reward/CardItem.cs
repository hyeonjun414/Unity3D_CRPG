using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardItem : Item
{
    public CardData cardData;
    public CardUI[] cards;

/*    protected override void Start()
    {
        base.Start();
        type = ItemType.Card;
        foreach (CardUI cardUI in cards)
        {
            cardUI.AddCard(cardData);
        }
    }*/

    public void SetUp(CardData data)
    {
        cardData = data;
        type = ItemType.Card;
        foreach (CardUI cardUI in cards)
        {
            cardUI.AddCard(cardData);
        }
    }

    public override void RewardGet()
    {
        base.RewardGet();
        CardManager.Instance.MoveCard(CardSpace.Field, CardSpace.Deck, cardData);
        Destroy(gameObject, 1f);
    }

    private void OnMouseEnter()
    {
        UIManager.Instance.cardInfoUI.InfoEnter(cardData);
    }
    private void OnMouseExit()
    {
        UIManager.Instance.cardInfoUI.InfoExit();
    }
}
