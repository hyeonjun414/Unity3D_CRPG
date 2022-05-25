using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealSkill : Skill
{
    public GameObject HealEffect;

    public override void SetUp(Monster monster, SkillData sd)
    {
        base.SetUp(monster, sd);

        StartCoroutine("CastingRoutine");

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
            list = BattleManager.Instance.allyMonster;
        }
        else
        {
            list = BattleManager.Instance.enemyMonster;
        }
        foreach(Monster m in list)
        {
            m.HP += 50;
            GameManager.Instance.CreateText(50, m.transform.position, TextType.Heal);
            Effect healEffect = ObjectPoolManager.Instance.UseObj(HealEffect).GetComponent<Effect>();
            healEffect.transform.SetParent(m.transform, true);
            healEffect.transform.position = m.transform.position + Vector3.up * 0.5f;
            healEffect.transform.rotation = Quaternion.identity;
        }
        UIManager.Instance.battleInfoUI.UpdateUI();
    }
}
