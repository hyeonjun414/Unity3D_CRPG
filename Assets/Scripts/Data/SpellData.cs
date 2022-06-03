using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpellType
{
    HealthUp,
    Meditation,
    Evolution,
    Honesty,
    Acceleration,
    Sacrifice,
    End

}
[CreateAssetMenu(fileName = "Spell Data", menuName = "Card/Spell Data")]
public class SpellData : CardData
{
    [Header("Spell Data")]
    public SpellType spellType;
}
