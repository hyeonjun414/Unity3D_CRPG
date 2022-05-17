using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardType
{
    Monster,
    Spell
}

[CreateAssetMenu(fileName = "Card Data", menuName = "Card/Card Data")]
public class CardData : ScriptableObject
{
    [Header("Card")]
    public CardType cardType;
    public Sprite icon;
    public new string name;
    public string desc;
    public int cost;
}
