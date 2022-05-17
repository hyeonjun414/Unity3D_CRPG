using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardUI : MonoBehaviour
{

    [Header("Card Data")]
    public CardData cardData;

    [Header("Card Units")]
    public MonsterCardUnit monsterCard;
    public BuffCardUnit buffCard;

    public void AddCard(CardData data)
    {
        cardData = data;
        gameObject.SetActive(true);

        switch (data.cardType)
        {
            case CardType.Monster:
                monsterCard.AddCard(data);
                buffCard.DeleteCard();
                break;
            case CardType.Spell:
                buffCard.AddCard(data);
                monsterCard.DeleteCard();
                break;
        }
    }
    public void DeleteCard()
    {
        cardData = null;
        transform.localPosition = new Vector3(1280, -80, 0);
        transform.localScale = Vector3.one * 0.1f;

        monsterCard.DeleteCard();
        buffCard.DeleteCard();

        gameObject.SetActive(false);

    }
}
