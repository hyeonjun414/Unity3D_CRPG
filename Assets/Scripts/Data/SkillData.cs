using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillType
{
    Heal,
    Fireball,
    CounterShield,
    EnergyDrain,
    DeathRay,
    ChainAttack,
    Throwing,
    Tornado,
    DropDown
}

[CreateAssetMenu(fileName = "Skill Data", menuName = "Skill/Skill Data")]
public class SkillData : ScriptableObject
{
    [Header("Skill status")]
    public SkillType skillType;
    public float effectInterval;
    public float effectCount;


    [Header("Skill Prefab")]
    public GameObject skillPrefab;
}
