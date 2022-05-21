using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSkillCommand : SkillCommand
{
    protected Monster monster;
    protected Animator anim;

    public SkillData skillData;
    public override void Setup(LivingEntity entity)
    {
        monster = (Monster)entity;
        anim = GetComponentInChildren<Animator>();
        skillData = monster.monsterData.skillData;
    }
    public override void Excute()
    {
        if (monster.target == null || monster.isMoving)
        {
            return;
        }
        Casting();
    }

    public virtual void Casting() 
    {
        monster.MP = 0;
        Skill skill = Instantiate(skillData.skillPrefab, transform.position, Quaternion.identity, monster.transform).GetComponent<Skill>();
        skill.SetUp(monster, skillData);
    }


}
