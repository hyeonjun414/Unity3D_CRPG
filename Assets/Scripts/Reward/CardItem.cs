using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardItem : RewardItem
{
    public CardData cardData;
    public CardUI[] cards;

    protected override void Start()
    {
        base.Start();
        type = RewardType.Card;
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
}
