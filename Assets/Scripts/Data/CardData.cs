using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardType
{
    Monster,
    Spell,
    End
}

[CreateAssetMenu(fileName = "Card Data", menuName = "Card/Card Data")]
public class CardData : ItemData
{
    [Header("Card")]
    public CardType cardType;
    public Sprite icon;
    public new string name;
    [TextArea]
    public string desc;
    public int cost;
}
