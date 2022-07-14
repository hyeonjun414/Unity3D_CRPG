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
    public string skillName;
    [TextArea]
    public string skillDesc;
    public float effectInterval;
    public float effectCount;

    [Header("Skill value")]
    public int skillValue;

    [Header("Skill Sfx")]
    public AudioClip skillSfx;

    [Header("Skill Prefab")]
    public GameObject skillPrefab;
}
