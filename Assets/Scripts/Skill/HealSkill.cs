using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealSkill : Skill
{
    public GameObject HealEffect;

    public override void SetUp(Monster monster, SkillData sd)
    {
        base.monster = monster;
        skillData = sd;
        skillName = skillData.skillName;
        skillDesc = skillData.skillDesc;
        skillType = skillData.skillType;
        skillLevel = 1;
        duration = skillData.duration;
        interval = skillData.interval;
        castingTime = skillData.castingTime;
        StartCoroutine("CastingRoutine");

    }
    public override IEnumerator CastingRoutine()
    {
        
        yield return new WaitForSeconds(0.1f);
        Casting();
    }
    public override void Casting()
    {
        Healing();
    }
    public void Healing()
    {
        List<Monster> list;
        if(monster.owner == MonsterOwner.Player)
        {
            list = StageManager.Instance.AllyMonster;
        }
        else
        {
            list = StageManager.Instance.EnemyMonster;
        }
        foreach(Monster m in list)
        {
            m.HP += 50;
            ParticleSystem go = Instantiate(HealEffect, m.transform.position + Vector3.up * 0.5f, Quaternion.identity, m.transform).GetComponent<ParticleSystem>(); ;
            Destroy(go.gameObject, go.main.duration);
        }
    }
}
