using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    public Monster monster;
    public string skillName;
    public string skillDesc;
    public SkillType skillType;
    public int skillLevel;
    public float duration;
    public float interval;
    public float castingTime;

    public SkillData skillData;

    public abstract void SetUp(Monster monster, SkillData sd);
    public abstract IEnumerator CastingRoutine();
    public abstract void Casting();
}
