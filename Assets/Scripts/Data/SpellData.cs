using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpellType
{
    None,
    Heal,
    Meditation,
    Evolution
}
[CreateAssetMenu(fileName = "Spell Data", menuName = "Card/Spell Data")]
public class SpellData : CardData
{
    [Header("Spell Data")]
    public SpellType spellType;
}
