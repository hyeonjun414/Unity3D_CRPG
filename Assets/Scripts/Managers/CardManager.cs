using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : Singleton<CardManager>
{
    public CardHolder holder;
    public DeckUI deckUI;

    public int maxHandCount = 5;
    public int maxDeckCount = 20;

    public List<CardData> deck;

    public CardUnit cardUnit;

    private void Awake()
    {
        if(_instance == null)
            _instance = this;
    }
    private void Start()
    {
        holder = UIManager.Instance.cardHolder;
        deckUI = UIManager.Instance.deckUI;

        
    }

    public void AddCard(CardData data)
    {
        if (deck.Count >= maxDeckCount) return;

        if (holder.cards.Count >= maxHandCount)
        {
            deckUI.AddDeckCard(deck, data);
        }
        else
        {
            CardUnit newCard = Instantiate(cardUnit, Vector3.down * 1000, Quaternion.identity);
            newCard.transform.SetParent(holder.transform, false);
            newCard.gameObject.transform.localScale = Vector3.one * 0.1f;
            newCard.AddCard(data);
            holder.cards.Insert(0, newCard);
        }

    }
}
