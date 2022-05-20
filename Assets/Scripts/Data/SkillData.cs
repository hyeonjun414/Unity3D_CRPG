using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillType
{
    Heal,
    Rain,
    Drain,
}

[CreateAssetMenu(fileName = "Skill Data", menuName = "Data/Skill Data")]
public class SkillData : ScriptableObject
{
    [Header("Skill status")]
    public string skillName;
    public string skillDesc;
    public SkillType skillType;
    public float duration;
    public float interval;
    public float castingTime;


    [Header("Skill Prefab")]
    public GameObject skillPrefab;
}
