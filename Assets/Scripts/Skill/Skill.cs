using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    public Monster monster;         // 스킬의 사용하는 몬스터
    public int skillLevel;          // 스킬의 레벨 = 몬스터의 레벨
    public float effectInterval;    // 스킬의 효과 간격
    public float effectCount;       // 스킬의 효과

    public virtual void SetUp(Monster monster, SkillData sd)
    {
        this.monster = monster;
        effectInterval = sd.effectInterval;
        effectCount = sd.effectCount;
        skillLevel = (int)monster.level;
        StartCoroutine("CastingRoutine");
    }
    public virtual IEnumerator CastingRoutine()
    {
        monster.isCasting = true;
        monster.anim.SetTrigger("Cast");
        for(int i = 0; i < effectCount; i++)
        {
            Casting();
            yield return new WaitForSeconds(effectInterval);
        }
        monster.isCasting = false;
        Destroy(gameObject);
    }
    public abstract void Casting();
}
