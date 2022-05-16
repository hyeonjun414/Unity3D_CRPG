using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Monster Data", menuName = "Card/Monster Data")]
public class MonsterData : CardData
{
    [Header("Monster Data")]
    public Monster monster;
}
