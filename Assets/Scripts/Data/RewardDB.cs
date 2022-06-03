using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RewardDB", menuName = "DB/RewardDB")]
public class RewardDB : ScriptableObject
{
    [Header("Monster")]
    public MonsterData[] monsters_lv1;
    public MonsterData[] monsters_lv2;
    public MonsterData[] monsters_lv3;

    [Header("Spell")]
    public SpellData[] spellData;

    [Header("Relic")]
    public RelicData[] relicData;
}
