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
        if (monster.target == null || monster.target.isVanished || monster.isMoving || monster.isCC || monster.isCasting)
        {
            return;
        }
        Casting();
    }

    public virtual void Casting() 
    {
        monster.MP = 0;
        GameManager.Instance.CreateText(skillData.skillName, transform.position, TextType.Skill);
        Skill skill = ObjectPoolManager.Instance.UseObj(skillData.skillPrefab).GetComponent<Skill>();
        skill.transform.position = transform.position;
        skill.transform.rotation = Quaternion.identity;
        skill.transform.SetParent(monster.transform, true);
        skill.SetUp(monster, skillData);
    }


}
